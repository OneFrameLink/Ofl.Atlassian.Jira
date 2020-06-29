using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Ofl.Net.Http.ApiClient.Json;
using Ofl.Threading.Tasks;

namespace Ofl.Atlassian.Jira.V3
{
    public class JiraClient : JsonApiClient, IJiraClient
    {
        #region Constructor

        public JiraClient(
            HttpClient httpClient,
            IOptions<JiraClientConfiguration> jiraClientConfigurationOptions
        ) : base(httpClient)
        {
            // Validate parameters.
            _jiraClientConfigurationOptions = jiraClientConfigurationOptions
                ?? throw new ArgumentNullException(nameof(jiraClientConfigurationOptions));
        }

        #endregion

        #region Instance, read-only state.

        private readonly IOptions<JiraClientConfiguration> _jiraClientConfigurationOptions;

        #endregion

        #region Implementation of IJiraClient

        public Task<GetWatchersResponse> GetWatchersAsync(
            GetWatchersRequest request, 
            CancellationToken cancellationToken
        )
        {
            // Validate parameters.
            if (request == null) throw new ArgumentNullException(nameof(request));

            // The url.
            string url = $"/issue/{ request.IssueIdOrKey }/watchers";

            // Get JSON.
            return GetAsync<GetWatchersResponse>(url, cancellationToken);
        }

        public Task RemoveWatcherAsync(
            RemoveWatcherRequest request, 
            CancellationToken cancellationToken
        )
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
            CreateOrUpdateRemoteIssueLinkRequest request, 
            CancellationToken cancellationToken
        )
        {
            // Validate parameters.
            if (request == null) throw new ArgumentNullException(nameof(request));

            // The url.
            string url = $"/issue/{ request.IssueIdOrKey }/remotelink";

            // Post.
            return PostAsync<RemoteIssueLink, CreateOrUpdateRemoteIssueLinkResponse>(
                url, request.RemoteIssueLink, cancellationToken);
        }

        public Task<GetIssueLinkTypesResponse> GetIssueLinkTypesAsync(
            CancellationToken cancellationToken
        )
        {
            // Validate parameters.

            // The URL.
            const string url = "/issueLinkType";

            // Get.
            return GetAsync<GetIssueLinkTypesResponse>(url, cancellationToken);
        }

        public Task LinkIssuesAsync(
            LinkIssuesRequest request, 
            CancellationToken cancellationToken
        )
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

        protected override ValueTask<string> FormatUrlAsync(
            string url, 
            CancellationToken cancellationToken
        )
        {
            // validate parameters.
            if (string.IsNullOrWhiteSpace(url)) throw new ArgumentNullException(nameof(url));

            // Create the API url.
            return ValueTaskExtensions.FromResult(_jiraClientConfigurationOptions.Value.CreateApiUri(url));
        }

        protected override async Task<HttpResponseMessage> ProcessHttpResponseMessageAsync(
            HttpResponseMessage httpResponseMessage, 
            CancellationToken cancellationToken
        )
        {
            // Validate parameters.
            if (httpResponseMessage == null) throw new ArgumentNullException(nameof(httpResponseMessage));

            // If the response code is valid, just return the response message.
            if (httpResponseMessage.IsSuccessStatusCode) return httpResponseMessage;

            // Deserailize the response into errors.
            ErrorCollection errorCollection = await httpResponseMessage
                .ToObjectAsync<ErrorCollection>(cancellationToken)
                .ConfigureAwait(false);

            // Throw an exception.
            throw new JiraException(errorCollection);
        }

        #endregion
    }
}
