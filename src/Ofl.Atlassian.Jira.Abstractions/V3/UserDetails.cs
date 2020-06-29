using System.Collections.Generic;

namespace Ofl.Atlassian.Jira.V3
{
    public class UserDetails
    {
        public string? Self { get; set; }

        public string? Name { get; set; }

        public string? Key { get; set; }

        public string? AccountId { get; set; }

        public string? EmailAddress { get; set; }

        public IReadOnlyDictionary<string, string>? AvatarUrls { get; set; }

        public string? DisplayName { get; set; }

        public bool Active { get; set; }

        public string? TimeZone { get; set; }

        public string? AccountType { get; set; }
    }
}
