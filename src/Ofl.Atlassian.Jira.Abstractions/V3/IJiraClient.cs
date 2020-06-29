using System.Threading;
using System.Threading.Tasks;

namespace Ofl.Atlassian.Jira.V3
{
    public interface IJiraClient
    {
        Task<GetWatchersResponse> GetWatchersAsync(
            GetWatchersRequest request, 
            CancellationToken cancellationToken
        );

        Task RemoveWatcherAsync(
            RemoveWatcherRequest request, 
            CancellationToken cancellationToken
        );

        Task<CreateOrUpdateRemoteIssueLinkResponse> CreateOrUpdateRemoteIssueLinkAsync(
            CreateOrUpdateRemoteIssueLinkRequest request, 
            CancellationToken cancellationToken
        );

        Task<GetIssueLinkTypesResponse> GetIssueLinkTypesAsync(
            CancellationToken cancellationToken
        );

        Task LinkIssuesAsync(
            LinkIssuesRequest request, 
            CancellationToken cancellationToken
        );
    }
}
