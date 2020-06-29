using System;

namespace Ofl.Atlassian.Jira.V3
{
    public class RemoveWatcherRequest
    {
        #region Constructor

        public RemoveWatcherRequest(
            string issueIdOrKey,
            string accountId
        )
        {
            // Validate parameters.
            IssueIdOrKey = string.IsNullOrWhiteSpace(issueIdOrKey)
                ? throw new ArgumentNullException(nameof(issueIdOrKey))
                : issueIdOrKey;
            AccountId = string.IsNullOrWhiteSpace(accountId)
                ? throw new ArgumentNullException(nameof(accountId))
                : accountId;
        }

        #endregion

        #region Instance, read-only state

        public string IssueIdOrKey { get; }

        public string AccountId { get; }

        #endregion
    }
}
