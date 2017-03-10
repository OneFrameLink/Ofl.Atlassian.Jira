using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Ofl.Core.Net.Http;

namespace Ofl.Atlassian.Jira.V2
{
    public class JiraClient : JsonApiClient, IJiraClient
    {
        #region Constructor

        public JiraClient(
            IHttpClientFactory httpClientFactory,
            IOptions<JiraClientConfiguration> configuration,
            ICredentialProvider credentialProvider) : base(httpClientFactory)
        {
            // Validate parameters.
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));
            if (credentialProvider == null) throw new ArgumentNullException(nameof(credentialProvider));

            // Assign values.
            _configuration = configuration.Value;
            _credentialProvider = credentialProvider;
        }

        #endregion

        #region Instance, read-only state.

        private readonly JiraClientConfiguration _configuration;

        private readonly ICredentialProvider _credentialProvider;

        #endregion

        #region Implementation of IJiraClient

        public Task<GetWatchersResponse> GetWatchersAsync(GetWatchersRequest request, CancellationToken cancellationToken)
        {
            // Validate parameters.
            if (request == null) throw new ArgumentNullException(nameof(request));

            // The url.
            string url = $"/issue/{ request.IssueIdOrKey }/watchers";

            // Get JSON.
            return GetAsync<GetWatchersResponse>(url, cancellationToken);
        }

        public Task RemoveWatcherAsync(RemoveWatcherRequest request, CancellationToken cancellationToken)
        {
            // Validate parameters.
            if (request == null) throw new ArgumentNullException(nameof(request));

            // The url.
            string url = $"/issue/{ request.IssueIdOrKey }/watchers";

            // Add the username.
            url = QueryHelpers.AddQueryString(url, "username", request.Username);

            // Delete.
            return DeleteAsync(url, cancellationToken);
        }

        public Task<CreateOrUpdateRemoteIssueLinkResponse> CreateOrUpdateRemoteIssueLinkAsync(
            CreateOrUpdateRemoteIssueLinkRequest request, CancellationToken cancellationToken)
        {
            // Validate parameters.
            if (request == null) throw new ArgumentNullException(nameof(request));

            // The url.
            string url = $"/issue/{ request.IssueIdOrKey }/remotelink";

            // Post.
            return PostAsync<RemoteIssueLink, CreateOrUpdateRemoteIssueLinkResponse>(url, request.RemoteIssueLink,
                cancellationToken);
        }

        public Task<GetIssueLinkTypesResponse> GetIssueLinkTypesAsync(CancellationToken cancellationToken)
        {
            // Validate parameters.

            // The URL.
            const string url = "/issueLinkType";

            // Get.
            return GetAsync<GetIssueLinkTypesResponse>(url, cancellationToken);
        }

        public Task LinkIssuesAsync(LinkIssuesRequest request, CancellationToken cancellationToken)
        {
            // Validate parameters.
            if (request == null) throw new ArgumentNullException(nameof(request));

            // The url.
            const string url = "/issueLink";

            // Post.
            return PostAsync(url, request, cancellationToken);
        }

        #endregion

        #region Overrides of JsonApiClient

        protected override async Task<HttpClient> CreateHttpClientAsync(CancellationToken cancellationToken)
        {
            // Call the overload.
            return await CreateHttpClientAsync(
                await _credentialProvider.CreateHttpMessageHandler(cancellationToken).ConfigureAwait(false),
                cancellationToken
            );
        }

        protected override Task<string> FormatUrlAsync(string url, CancellationToken cancellationToken)
        {
            // validate parameters.
            if (string.IsNullOrWhiteSpace(url)) throw new ArgumentNullException(nameof(url));

            // Create the API url.
            return Task.FromResult(_configuration.CreateApiUri(url));
        }

        #region Overrides of ApiClient

        protected override async Task<HttpResponseMessage> ProcessHttpResponseMessageAsync(HttpResponseMessage httpResponseMessage,
            JsonSerializerSettings jsonSerializerSettings, CancellationToken cancellationToken)
        {
            // Validate parameters.
            if (httpResponseMessage == null) throw new ArgumentNullException(nameof(httpResponseMessage));
            if (jsonSerializerSettings == null) throw new ArgumentNullException(nameof(jsonSerializerSettings));

            // If the response code is valid, just return the response message.
            if (httpResponseMessage.IsSuccessStatusCode) return httpResponseMessage;

            // Deserailize the response into errors.
            ErrorCollection errorCollection = await httpResponseMessage.ToObjectAsync<ErrorCollection>(
                jsonSerializerSettings, cancellationToken).ConfigureAwait(false);

            // Throw an exception.
            throw new JiraException(errorCollection);
        }

        #endregion

        #endregion
    }
}
