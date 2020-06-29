using System;

namespace Ofl.Atlassian.Jira.V3
{
    public class GetWatchersRequest
    {
        #region Constructor

        public GetWatchersRequest(
            string issueIdOrKey
        )
        {
            // Validate parameters.
            IssueIdOrKey = string.IsNullOrWhiteSpace(issueIdOrKey)
                ? throw new ArgumentNullException(nameof(issueIdOrKey))
                : issueIdOrKey;
        }

        #endregion

        #region Instance, read-only state

        public string IssueIdOrKey { get; }

        #endregion
    }
}
