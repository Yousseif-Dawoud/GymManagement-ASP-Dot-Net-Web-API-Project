namespace Gym.Application.Services;

public sealed class MemberService : IMemberService
{
    // Inject The UnitOfWork To Access The Repositories And Perform Database Operations
    private readonly IUnitOfWork _uow;
    public MemberService( IUnitOfWork uow) => _uow = uow;


    // Create A New Member
    // ----------------------------------
    public async Task<MemberResponse> CreateAsync(CreateMemberRequest request, CancellationToken ct = default)
    {
        // 1. Check Email uniqueness 
        await EnsureEmailIsUniqueAsync(request.Email, ct);



        // 2. Check Phone uniqueness
        await EnsurePhoneIsUniqueAsync(request.Phone, ct);



        // 3. Check MembershipPlan exists if not throw NotFoundException
        var membershipPlan = await GetMembershipPlanOrThrowAsync(request.MembershipPlanId, ct);



        // 4. Check Package exists (optional)  Focus I Say The Package is Optional .
        var package = await GetPackageOrThrowAsync(request.PackageId,ct);



        // 5. Normalize Email and Phone Before Storing In The Database.
        var normalizedEmail = request.Email.Trim().ToLowerInvariant();
        var normalizedPhone = request.Phone.Trim();



        // 6. Create Member Entity
        var member = new Member(
                     request.FullName,
                     normalizedPhone,
                     normalizedEmail,
                     request.Gender,
                     request.DateOfBirth,
                     request.EmergencyContact,
                     request.MembershipStartDate,
                     request.MembershipEndDate,
                     request.MembershipPlanId,
                     request.PackageId
                     );




        // 7. Add Member to Repository and Save Changes
        await _uow.Members.AddAsync(member, ct);
        await _uow.SaveChangesAsync(ct);



        // 8. Map response
        return ToResponse(member,membershipPlan.Type.ToString(),package?.Name);
    }
    


    public Task AssignPackageAsync(int memberId, int packageId, CancellationToken ct = default)
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



    // Private Helper Methods For Business Rule Validations Can Be Added Here

    private async Task EnsureEmailIsUniqueAsync(string email,CancellationToken ct)
    {
        var normalizedEmail = email.Trim().ToLowerInvariant();

        var exists = await _uow.Members.ExistsAsync(m => m.Email == normalizedEmail,ct);

        if (exists)
            throw new BusinessException("A member with this email already exists.");
    }

    private async Task EnsurePhoneIsUniqueAsync(string phone, CancellationToken ct)
    {
        var normalizedPhone = phone.Trim();

        var exists = await _uow.Members.ExistsAsync(m => m.Phone == normalizedPhone, ct);

        if (exists)
            throw new BusinessException("A member with this phone number already exists.");
    }

    private async Task<MembershipPlan> GetMembershipPlanOrThrowAsync (int membershipPlanId, CancellationToken ct)
    {
        var membershipPlan = await _uow.MembershipPlans.GetByIdAsync(membershipPlanId, ct);

        if (membershipPlan is null)  throw new NotFoundException("Membership plan was not found.");

        return membershipPlan;
    }

    private async Task<Package?> GetPackageOrThrowAsync(int? packageId,CancellationToken ct)
    {
        if ( ! packageId.HasValue)  return null;

        var package = await _uow.Packages.GetByIdAsync(packageId.Value,ct);

        if (package is null)   throw new NotFoundException("Package was not found.");
        
        return package;
    }

    private static MemberResponse ToResponse(Member member,string membershipPlanName,string? packageName)
    {
        return new MemberResponse(
            member.Id,
            member.FullName,
            member.Phone,
            member.Email,
            member.Gender,
            member.DateOfBirth,
            member.EmergencyContact,
            member.Status,
            member.MembershipStartDate,
            member.MembershipEndDate,
            member.MembershipPlanId,
            membershipPlanName,
            member.PackageId,
            packageName
        );
    }
}
