namespace Ofl.Atlassian.Jira.V3
{
    public class LinkIssuesRequest
    {
        public IssueLinkType? Type { get; set; }

        public IssueReference? InwardIssue { get; set; }

        public IssueReference? OutwardIssue { get; set; }

        public Comment? Comment { get; set; }
    }
}
