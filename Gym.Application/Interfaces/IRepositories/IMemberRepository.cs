namespace Gym.Application.Interfaces.IRepositories;


// Specific repository : generic + custom queries
public interface IMemberRepository : IGenericRepository<Member>
{
    Task<PagedResult<MemberListItem>> SearchAsync(MemberQueryRequest request,CancellationToken ct = default);

    Task<MemberProfileResponse?> GetProfileAsync(int memberId,CancellationToken ct = default);
}