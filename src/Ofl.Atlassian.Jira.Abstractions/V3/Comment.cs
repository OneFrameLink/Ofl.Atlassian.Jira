namespace Ofl.Atlassian.Jira.V3
{
    // NOTE: Incomplete.
    public class Comment
    {
        public string? Self { get; set; }

        public string? Id { get; set; }

        public UserDetails? Author { get; set; }

        public string? Body { get; set; }

        public string? RenderedBody { get; set; }

        public UserDetails? UpdateAuthor { get; set; }

        public string? Created { get; set; }

        public string? Updated { get; set; }
    }
}
