


namespace Gym.Application.Services;

public sealed class MemberService : IMemberService
{
    private readonly IUnitOfWork _uow;
    public MemberService( IUnitOfWork uow) => _uow = uow;




    // Create A New Member From The Request DTO Data And Return The Response DTO Data
    // -----------------------------------------------------------------------------------------------
    public async Task<MemberResponse> CreateAsync(CreateMemberRequest request, CancellationToken ct = default)
    {
        // 1. Validate the MembershipPlanId exists
        var plan = await _uow.MembershipPlans.GetByIdAsync(request.MembershipPlanId, ct);

        if(plan is null)
        {
            throw new NotFoundException($"MembershipPlan with id {request.MembershipPlanId} was not found.");
        }

        // 2. Create a new Member entity using the request data
        var member = new Member(
            fullName: request.FullName,
            phone: request.Phone,
            email: request.Email,
            startDate: request.MembershipStartDate,
            endDate: request.MembershipEndDate,
            membershipPlanId: request.MembershipPlanId
        );


        // 3. Add the new Member to the database and save changes
        await _uow.Members.AddAsync(member,ct );
        await _uow.SaveChangesAsync( ct );


        // 4. Map the created Member entity to a MemberResponse DTO and return it
        return MapToResponse(member, planName: plan.Name);
    }
    // -----------------------------------------------------------------------------------------------




    // Get All Members And Return A List Of MemberListItem DTOs 
    // -----------------------------------------------------------------------------------------------
    public async Task<IReadOnlyList<MemberListItem>> GetAllMembers(CancellationToken ct = default)
    {
        // 1. Retrieve all Member entities from the database
        var members = await _uow.Members.GetAllAsync(ct);


        // 2. Map each Member entity to a MemberListItem DTO and return the list of DTOs
        return members
               .Select(m => new MemberListItem(
                   Id: m.Id,
                   FullName: m.FullName,
                   Phone: m.Phone,
                   Status: m.Status.ToString()
               ))
               .ToList();
    }
    // -----------------------------------------------------------------------------------------------




    // Get A Member By Id And Return A MemberResponse DTO 
    // -----------------------------------------------------------------------------------------------
    public async Task<MemberResponse> GetByIdAsync(int id, CancellationToken ct = default)
    {
        // 1. Retrieve the Member entity with the specified id from the database
        var member = await _uow.Members.GetByIdAsync(id, ct);


        // 2. If the Member entity is not found, throw a NotFoundException
        if(member is null)
            throw new NotFoundException($"Member with id {id} was not found.");


        // 3. Retrieve the name of the MembershipPlan associated with the Member
        var plan = await _uow.MembershipPlans.GetByIdAsync(member.MembershipPlanId, ct);


        // 4. Handel The PlanName If The Plan Name Is Null Set It To "Unknown Plan"
        var planName = plan is not null ? plan.Name : "Unknown Plan";


        // 5. Map the Member entity to a MemberResponse DTO and return it
        return MapToResponse(member, planName);
    }
    // -----------------------------------------------------------------------------------------------
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
