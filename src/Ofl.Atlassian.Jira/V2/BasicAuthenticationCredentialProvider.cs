using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace Ofl.Atlassian.Jira.V2
{
    public class BasicAuthenticationCredentialProvider : ICredentialProvider
    {
        #region Constructor

        public BasicAuthenticationCredentialProvider(IOptions<JiraClientConfiguration> configuration)
        {
            // Validate parameters.
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            // Assign values.
            _configuration = configuration.Value;
        }

        #endregion

        #region Instance, read-only state.

        private readonly JiraClientConfiguration _configuration;

        #endregion

        #region Implementation of ICredentialProvider

        public Task<HttpMessageHandler> CreateHttpMessageHandler(CancellationToken cancellationToken)
        {
            // Create the handler and return.
            return Task.FromResult((HttpMessageHandler) new BasicAuthenticationHttpMessageHandler(_configuration));
        }

        #endregion
    }
}
