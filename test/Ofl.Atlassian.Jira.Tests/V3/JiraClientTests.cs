using Ofl.Atlassian.Jira.V3;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Ofl.Atlassian.Jira.Tests.V3
{
    public class JiraClientTests : IClassFixture<JiraClientTestsFixture>
    {
        #region Constructor

        public JiraClientTests(JiraClientTestsFixture fixture)
        {
            // Validate parameters.
            _fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));
        }

        #endregion

        #region Instance, read-only state.

        private readonly JiraClientTestsFixture _fixture;

        #endregion

        #region Helpers

        private IJiraClient CreateJiraClient() => _fixture.CreateJiraClient();

        #endregion

        #region Tests

        [Fact]
        public async Task Test_GetIssueLinkTypes_Async()
        {
            // Create the client.
            IJiraClient client = CreateJiraClient();

            // Make the call.
            GetIssueLinkTypesResponse response = await client
                .GetIssueLinkTypesAsync(CancellationToken.None)
                .ConfigureAwait(false);

            // Assert.
            Assert.NotEmpty(response.IssueLinkTypes);
        }

        #endregion
    }
}
