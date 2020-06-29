using System;

namespace Ofl.Atlassian.Jira.V3
{
    public class CreateOrUpdateRemoteIssueLinkRequest
    {
        #region Constructor

        public CreateOrUpdateRemoteIssueLinkRequest(
            string issueIdOrKey,
            RemoteIssueLink remoteIssueLink
        )
        {
            // Validate parameters.
            IssueIdOrKey = string.IsNullOrWhiteSpace(issueIdOrKey)
                ? throw new ArgumentNullException(nameof(issueIdOrKey))
                : issueIdOrKey;
            RemoteIssueLink = remoteIssueLink
                ?? throw new ArgumentNullException(nameof(remoteIssueLink));
        }

        #endregion

        #region Instance, read-only state.

        public string IssueIdOrKey { get; set; }

        public RemoteIssueLink RemoteIssueLink { get; set; }

        #endregion
    }
}
