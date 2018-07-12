using System;

namespace Ofl.Atlassian.Jira.V2
{
    public class JiraClientConfiguration
    {
        public Uri BaseUrl { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}
