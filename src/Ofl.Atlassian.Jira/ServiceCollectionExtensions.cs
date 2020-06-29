using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ofl.Atlassian.Jira.V3;

namespace Ofl.Atlassian.Jira
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAtlassianJiraClient(
            this IServiceCollection serviceCollection,
            IConfiguration jiraClientConfigurationOptions)
        {
            // Validate parameters.
            var sc = serviceCollection ?? throw new ArgumentNullException(nameof(serviceCollection));
            if (jiraClientConfigurationOptions == null) throw new ArgumentNullException(nameof(jiraClientConfigurationOptions));

            // Set up the client configuration options.
            sc = sc.Configure<JiraClientConfiguration>(jiraClientConfigurationOptions.Bind);
            
            // Set up the message handler.
            sc = sc.AddTransient<BasicAuthenticationHttpMessageHandler>();

            // Add the JIRA client.
            sc.AddHttpClient<IJiraClient, JiraClient>()
                .ConfigurePrimaryHttpMessageHandler<BasicAuthenticationHttpMessageHandler>();

            // Return the service collection.
            return sc;
        }
    }
}
