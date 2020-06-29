namespace Ofl.Atlassian.Jira.V3
{
    public class RemoteIssueLink
    {
        public string? GlobalId { get; set; }

        public Application? Application { get; set; }

        public string? Relationship { get; set; }

        public RemoteObject? Object { get; set; }
    }
}
