using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ofl.Atlassian.Jira.V3;

namespace Ofl.Atlassian.Jira.Tests.V3
{
    public class JiraClientTestsFixture : IDisposable
    {
        #region Constructor

        public JiraClientTestsFixture()
        {
            // Assign values.
            _serviceProvider = CreateServiceProvider();
        }

        #endregion

        #region Read-only state.

        private static readonly IConfigurationRoot ConfigurationRoot = new ConfigurationBuilder()
            // Always used.
            .AddJsonFile("appsettings.json")
            // For local debugging.
            .AddJsonFile("appsettings.local.json", true)
            // For Appveyor.
            .AddEnvironmentVariables()
            .Build();

        private readonly ServiceProvider _serviceProvider;

        #endregion

        #region Helpers

        private static ServiceProvider CreateServiceProvider()
        {
            // Create a collection.
            var sc = new ServiceCollection();

            // Add the google apis.
            sc.AddAtlassianJiraClient(ConfigurationRoot.GetSection("jira"));

            // Build and return.
            return sc.BuildServiceProvider();
        }

        public IJiraClient CreateJiraClient() => _serviceProvider.GetRequiredService<IJiraClient>();

        #endregion

        #region IDisposable implementation.

        public void Dispose()
        {
            // Call the overload and
            // suppress finalization.
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            // Dispose of unmanaged resources.

            // If not disposing, get out.
            if (!disposing) return;

            // Dispose of IDisposable implementations.
            using var _ = _serviceProvider;
        }

        ~JiraClientTestsFixture() => Dispose(false);

        #endregion
    }
}
