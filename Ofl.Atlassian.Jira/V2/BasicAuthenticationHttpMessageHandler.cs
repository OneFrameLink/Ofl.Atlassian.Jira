using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Ofl.Core.Net.Http;

namespace Ofl.Atlassian.Jira.V2
{
    internal class BasicAuthenticationHttpMessageHandler : HttpClientHandler
    {
        #region Constructor

        internal BasicAuthenticationHttpMessageHandler(JiraClientConfiguration configuration)
        {
            // Validate parameters.
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            // Assign values.
            _configuration = configuration;

            // Set compression.
            this.SetCompression();
        }

        #endregion

        #region Instance, read-only state.

        private readonly JiraClientConfiguration _configuration;

        #endregion

        #region Overrides

        #region Overrides of HttpMessageHandler

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Validate parameters.
            if (request == null) throw new ArgumentNullException(nameof(request));

            // Set the username and password on the request.
            request.SetBasicHttpAuthentication(_configuration.Username, _configuration.Password);

            // Send the request.
            return base.SendAsync(request, cancellationToken);
        }

        #endregion

        #endregion
    }
}
