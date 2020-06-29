using System.Collections.Generic;

namespace Ofl.Atlassian.Jira.V3
{
    public class GetWatchersResponse
    {
        public string? Self { get; set; }

        public bool IsWatching { get; set; }

        public int WatchCount { get; set; }

        public IReadOnlyCollection<UserDetails>? Watchers { get; set; }
    }
}
