


using Gym.Application.Common;

namespace Gym.Application.Services;

public sealed class MemberService : IMemberService
{
    // Inject The UnitOfWork To Access The Repositories And Perform Database Operations
    private readonly IUnitOfWork _uow;
    public MemberService( IUnitOfWork uow) => _uow = uow;

    public Task AssignPackageAsync(int memberId, int packageId, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task<MemberResponse> CreateAsync(CreateMemberRequest request, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int memberId, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task ExpireMembershipAsync(int memberId, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task FreezeMembershipAsync(int memberId, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task<MemberResponse?> GetByIdAsync(int memberId, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task<MemberProfileResponse> GetProfileAsync(int memberId, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task RemovePackageAsync(int memberId, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task RenewMembershipAsync(int memberId, RenewMembershipRequest request, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task<PagedResult<MemberListItem>> SearchAsync(MemberQueryRequest request, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task UnfreezeMembershipAsync(int memberId, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task<MemberResponse> UpdateAsync(int memberId, UpdateMemberRequest request, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}
