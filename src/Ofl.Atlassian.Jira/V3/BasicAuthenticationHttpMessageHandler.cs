using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Ofl.Net.Http;

namespace Ofl.Atlassian.Jira.V3
{
    public class BasicAuthenticationHttpMessageHandler : HttpClientHandler
    {
        #region Constructor

        public BasicAuthenticationHttpMessageHandler(IOptions<JiraClientConfiguration> jiraClientConfigurationOptions)
        {
            // Validate parameters.
            _jiraClientConfigurationOptions = jiraClientConfigurationOptions ?? throw new ArgumentNullException(nameof(jiraClientConfigurationOptions));

            // Set compression.
            this.SetCompression();
        }

        #endregion

        #region Instance, read-only state.

        private readonly IOptions<JiraClientConfiguration> _jiraClientConfigurationOptions;

        #endregion

        #region Overrides

        #region Overrides of HttpMessageHandler

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Validate parameters.
            if (request == null) throw new ArgumentNullException(nameof(request));

            // Get the current config.
            JiraClientConfiguration config = _jiraClientConfigurationOptions.Value;

            // Set the username and password on the request.
            request.SetBasicHttpAuthentication(config.Username, config.Password);

            // Send the request.
            return base.SendAsync(request, cancellationToken);
        }

        #endregion

        #endregion
    }
}
