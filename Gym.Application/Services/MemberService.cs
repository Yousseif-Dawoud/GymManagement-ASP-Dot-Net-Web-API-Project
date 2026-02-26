


namespace Gym.Application.Services;

public sealed class MemberService : IMemberService
{
    private readonly IUnitOfWork _uow;
    public MemberService( IUnitOfWork uow) => _uow = uow;

    // Create A New Member From The Request DTO Data And Return The Response DTO Data
    // -----------------------------------------------------------------------------------------------
    public async Task<MemberResponse> CreateAsync(CreateMemberRequest request, CancellationToken ct = default)
    {
        var plan = await _uow.MembershipPlans.GetByIdAsync(request.MembershipPlanId, ct);

        if(plan is null)
        {
            throw new NotFoundException($"MembershipPlan with id {request.MembershipPlanId} was not found.");
        }

        var member = new Member(
            fullName: request.FullName,
            phone: request.Phone,
            email: request.Email,
            startDate: request.MembershipStartDate,
            endDate: request.MembershipEndDate,
            membershipPlanId: request.MembershipPlanId
        );

        await _uow.Members.AddAsync(member,ct );
        await _uow.SaveChangesAsync( ct );

        return MapToResponse(member, planName: plan.Name);
    }
    // -----------------------------------------------------------------------------------------------


    public Task<IReadOnlyList<MemberListItem>> GetAllMembers(CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task<MemberResponse> GetByIdAsync(int id, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task<MemberResponse> GetProfileAsync(int id, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }


    private static MemberResponse MapToResponse(Member member, string planName)
        => new(
            Id: member.Id,
            FullName: member.FullName,
            Phone: member.Phone,
            Email: member.Email,
            MembershipStartDate: member.MembershipStartDate,
            MembershipEndDate: member.MembershipEndDate,
            MembershipPlanId: member.MembershipPlanId,
            MembershipPlanName: planName,
            Status: member.Status.ToString(),
            CreatedAt: member.CreatedAt,
            UpdatedAt: member.UpdatedAt
        );
}
