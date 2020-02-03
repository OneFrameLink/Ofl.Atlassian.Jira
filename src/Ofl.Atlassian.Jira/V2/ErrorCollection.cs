using System.Collections.Generic;

namespace Ofl.Atlassian.Jira.V2
{
    public class ErrorCollection
    {
        public IReadOnlyCollection<string>? ErrorMessages { get; set; }

        public IReadOnlyDictionary<string, string>? Errors { get; set; }

        public int Status { get; set; }
    }
}
