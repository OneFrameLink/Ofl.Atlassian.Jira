using System;
using System.Net.Http;

namespace Ofl.Atlassian.Jira.V2
{
    public class JiraException : HttpRequestException
    {
        #region Constructors

        public JiraException(ErrorCollection errorCollection)
        {
            // Validate parameters
            ErrorCollection = errorCollection
                ?? throw new ArgumentNullException(nameof(errorCollection));
        }

        #endregion

        #region Instance, read-only state.

        public ErrorCollection ErrorCollection { get; }

        #endregion
    }
}
