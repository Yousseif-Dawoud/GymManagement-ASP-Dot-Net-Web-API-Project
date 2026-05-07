


namespace Gym.Application.Services;

public sealed class MemberService : IMemberService
{
    // Inject The UnitOfWork To Access The Repositories And Perform Database Operations
    private readonly IUnitOfWork _uow;
    public MemberService( IUnitOfWork uow) => _uow = uow;



    // Create A New Member From The Request DTO Data And Return The Response DTO Data
    public async Task<MemberResponse> CreateAsync(CreateMemberRequest request, CancellationToken ct = default)
    {
        // 1. Validation : To Check If The MembershipPlanId There Exists In The Database Or Not .
        MembershipPlan plan = await GetMembershipPlanAsync(request.MembershipPlanId, ct);


        // 2. Make Email Normalization To Help Me Make Check On the Emails In the Database .
        var emailNormalized = request.Email.Trim().ToLowerInvariant();


        // 3. Make Check If The Email Already Exists In The Database Or Not .
        await EnsureEmailIsUniqueAsync(request ,  emailNormalized, ct);


        // 4. Create A New Member Entity Using The Data From The Request DTO 
        var member = new Member(
            fullName: request.FullName,
            phone: request.Phone,
            email: emailNormalized,
            startDate: request.MembershipStartDate,
            endDate: request.MembershipEndDate,
            membershipPlanId: request.MembershipPlanId
        );


        // 5. Add The New Member Entity To The Database And Save Changes
        await _uow.Members.AddAsync(member, ct);
        await _uow.SaveChangesAsync(ct);


        // 6. Map The Created Member Entity To A MemberResponse DTO And Return It
        return MapToResponse(member, planName: plan.Name);
    }



    // Get All Members And Return A List Of MemberListItem DTOs 
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



    // Get A Member By Id And Return A MemberResponse DTO 
    public async Task<MemberResponse> GetByIdAsync(int id, CancellationToken ct = default)
    {
        if (id <= 0)
            throw new BadRequestException("Id must be a positive integer.");

        // 1. Retrieve the Member entity with the specified id from the database
        var member = await _uow.Members.GetByIdAsync(id, ct);


        // 2. If the Member entity is not found, throw a NotFoundException
        if (member is null)
            throw new NotFoundException($"Member with id {id} was not found.");


        // 3. Retrieve the name of the MembershipPlan associated with the Member
        var plan = await _uow.MembershipPlans.GetByIdAsync(member.MembershipPlanId, ct);


        // 4. Handel The PlanName If The Plan Name Is Null Set It To "Unknown Plan"
        var planName = plan is not null ? plan.Name : "Unknown Plan";


        // 5. Map the Member entity to a MemberResponse DTO and return it
        return MapToResponse(member, planName);
    }



    // Update A Member By Id Using The Request DTO Data And Return The Updated Member Details DTO
    public async Task<MemberResponse> UpdateAsync(int id, UpdateMemberRequest request, CancellationToken ct = default)
    {
        //  Retrieve the Member entity with the specified id from the database
        var member = await _uow.Members.GetByIdAsync(id, ct);


        //  If the Member entity is not found, throw a NotFoundException
        if (member is null)
            throw new NotFoundException($"Member with id {id} was not found.");


        // Check If The MembershipPlanId There Exists In The Database Or Not .
        MembershipPlan plan = await GetMembershipPlanAsync(request.MembershipPlanId, ct);


        //  Update the Member entity with the data from the Request DTO
        member.Update(
            fullName: request.FullName,
            phone: request.Phone,
            email: request.Email,
            membershipStartDate: request.MembershipStartDate,
            membershipEndDate: request.MembershipEndDate,
            membershipPlanId: request.MembershipPlanId
        );


        //  Save the changes to the database
        _uow.Members.Update(member);


        //  Save the changes to the database
        await _uow.SaveChangesAsync(ct);


        // Handel The PlanName If The Plan Name Is Null Set It To "Unknown Plan"
        var planName = plan is not null ? plan.Name : "Unknown Plan";


        //  Map the updated Member entity to a MemberResponse DTO and return it
        return MapToResponse(member, planName);
    }



    // Get A Member's Profile By Id And Return A MemberResponse DTO
    public async Task<MemberResponse> GetProfileAsync(int id, CancellationToken ct = default)
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





    // ===================== Helper Methods =====================

    // This Method Is Used To Retrieve A MembershipPlan Entity By Its Id From The Database,
    private async Task<MembershipPlan> GetMembershipPlanAsync(int membershipPlanId, CancellationToken ct = default)
    {
        // 1. Retrieve the MembershipPlan entity with the specified id from the database
        var plan = await _uow.MembershipPlans.GetByIdAsync(membershipPlanId, ct);


        // 2. If the MembershipPlan entity is not found, throw a NotFoundException
        if (plan is null)
            throw new NotFoundException($"Membership plan with ID {membershipPlanId} not found.");

        return plan;
    }


    // This Method Is Used To Ensure That The Email Is Unique In The Database
    private async Task EnsureEmailIsUniqueAsync(CreateMemberRequest request, string emailNormalized, CancellationToken ct)
    {
        var allMembers = await _uow.Members.GetAllAsync(ct);

        var exists = allMembers.Any(m => m.Email != null && m.Email == emailNormalized);

        if (exists)
            throw new ConflictException($"Email '{request.Email}' is already used.");
    }


    // This Method Is Used To Map A Member Entity To A MemberResponse DTO,
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

    // ===========================================================
}
