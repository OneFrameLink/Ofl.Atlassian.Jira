using System;

namespace Ofl.Atlassian.Jira.V3
{
    public class RemoveWatcherRequest
    {
        #region Constructor

        public RemoveWatcherRequest(
            string issueIdOrKey,
            string username
        )
        {
            // Validate parameters.
            IssueIdOrKey = string.IsNullOrWhiteSpace(issueIdOrKey)
                ? throw new ArgumentNullException(nameof(issueIdOrKey))
                : issueIdOrKey;
            Username = string.IsNullOrWhiteSpace(username)
                ? throw new ArgumentNullException(nameof(username))
                : username;
        }

        #endregion

        #region Instance, read-only state

        public string IssueIdOrKey { get; }

        public string Username { get; }

        #endregion
    }
}
