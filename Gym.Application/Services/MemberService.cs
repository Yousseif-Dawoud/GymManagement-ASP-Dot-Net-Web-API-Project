namespace Gym.Application.Services;

public sealed class MemberService : IMemberService
{
    // Inject The UnitOfWork To Access The Repositories And Perform Database Operations
    private readonly IUnitOfWork _uow;
    public MemberService(IUnitOfWork uow) { _uow = uow; }



    // Write Operations  { CreateAsync , UpdateAsync , DeleteAsync }  
    // ===============================


    // Create A New Member
    // -------------------
    public async Task<MemberResponse> CreateAsync(CreateMemberRequest request, CancellationToken ct = default)
    {
        // 1. Normalize Email and Phone To Use Them In Uniqueness Checks And When The Checks Is Valide , I Can Save them.
        var normalizedEmail = request.Email.Trim().ToLowerInvariant();
        var normalizedPhone = request.Phone.Trim();



        // 2. Check Email uniqueness 
        await EnsureEmailIsUniqueAsync(normalizedEmail, null, ct);



        // 3. Check Phone uniqueness
        await EnsurePhoneIsUniqueAsync(normalizedPhone, null, ct);



        // 4. Check MembershipPlan exists if not throw NotFoundException
        var membershipPlan = await GetMembershipPlanOrThrowAsync(request.MembershipPlanId, ct);



        // 5. Check Package exists (optional)  Focus I Say The Package is Optional .
        var package = await GetPackageOrThrowAsync(request.PackageId,ct);



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


    // Update Member Information
    // -------------------
    public async Task<MemberResponse> UpdateAsync(int memberId, UpdateMemberRequest request, CancellationToken ct = default)
    {
        // 1. Get Member By Id Or Throw NotFoundException If Not Found.
        var member = await GetMemberOrThrowAsync(memberId, ct);



        // 2. Normalize Email and Phone To Use Them In Uniqueness Checks And When The Checks Is Valide , I Can Save them.
        var normalizedEmail = request.Email.Trim().ToLowerInvariant();
        var normalizedPhone = request.Phone.Trim();



        // 3. Check Email uniqueness (exclude current member)
        await EnsureEmailIsUniqueAsync(normalizedEmail, memberId, ct);



        // 4. Check Phone uniqueness (exclude current member)
        await EnsurePhoneIsUniqueAsync(normalizedPhone, memberId, ct);



        // 5. Check MembershipPlan exists if not throw NotFoundException
        var membershipPlan = await GetMembershipPlanOrThrowAsync(request.MembershipPlanId, ct);



        // 6. Check Package exists (optional)  Focus I Say The Package is Optional .
        var package = await GetPackageOrThrowAsync(request.PackageId, ct);



        // 7. Update Member Entity
        member.Update(
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



        // 8. Save Changes
        await _uow.SaveChangesAsync(ct);

        // Why I Don't Need To Call An Update Method On The Repository ?  Focus I Say Because We Are Tracking The Entity We Fetched From The Database So Any Changes We Make To It Will Be Automatically Detected And Saved When We Call SaveChangesAsync. This Is One Of The Key Benefits Of Using An ORM Like Entity Framework - It Handles Change Tracking For Us, So We Don't Have To Manually Tell It When An Entity Has Been Updated. We Just Fetch The Entity, Modify Its Properties, And Then Call SaveChangesAsync To Persist Those Changes To The Database.



        // 9. Map response
        return ToResponse(member, membershipPlan.Type.ToString(), package?.Name);
    }


    // Delete Member By Id
    // -------------------
    public async Task DeleteAsync(int memberId, CancellationToken ct = default)
    {

        // TODO:
        // Replace hard delete with soft delete once the project
        // supports audit logging and historical data retention.

        // 1. Get Member By Id Or Throw NotFoundException If Not Found.
        var member = await GetMemberOrThrowAsync(memberId, ct);


        // 2. Ensure Member Has No Bookings
        await EnsureMemberHasNoBookingsAsync(memberId, ct);


        // 3. Delete Member
        _uow.Members.Remove(member);


        // 4. Save Changes
        await _uow.SaveChangesAsync(ct);
    }






    // Read Operations  { GetByIdAsync , SearchAsync , GetProfileAsync }
    // ===============================


    // Get Member By Id
    // -------------------
    public async Task<MemberResponse> GetByIdAsync(int memberId,CancellationToken ct = default)
    {
        // 1. Get Member By Id Or Throw NotFoundException If Not Found.
        var member = await GetMemberOrThrowAsync(memberId, ct);



        // 2. Get MembershipPlan Or Throw NotFoundException If Not Found.
        var membershipPlan = await GetMembershipPlanOrThrowAsync(member.MembershipPlanId, ct);



        // 3. Get Package If Assigned (Optional) Focus I Say The Package Is Optional .
        var package = await GetPackageOrThrowAsync(member.PackageId, ct);



        // 4. If All Good Map The Member To MemberResponse And Return It.
        return ToResponse(member, membershipPlan.Type.ToString(), package?.Name);
    }


    // Search Members With Pagination And Filtering
    // -------------------
    public Task<PagedResult<MemberListItem>> SearchAsync(MemberQueryRequest request,CancellationToken ct = default)
    {
        return _uow.Members.SearchAsync(request, ct);
    }


    // Get Member Profile
    // -------------------
    public async Task<MemberProfileResponse> GetProfileAsync(int memberId, CancellationToken ct = default)
    {
        // Get The Member Profile From The Repository And If Not Found Throw NotFoundException.
        return await _uow.Members.GetProfileAsync(memberId, ct) 
                                  ?? throw new NotFoundException("Member was not found.");
    }






    // Membership Management Operations
    // ===============================


    // Assign A Package To A Member
    // -------------------
    public async Task AssignPackageAsync(int memberId,int packageId,CancellationToken ct = default)
    {
        // 1. Get Member By Id Or Throw NotFoundException If Not Found.
        var member = await GetMemberOrThrowAsync(memberId, ct);


        // 2. Get Package By Id Or Throw NotFoundException If Not Found.
        var package = await GetPackageOrThrowAsync(packageId, ct);


        // 3. Ensure member doesn't already have this package
        if (member.PackageId == packageId)
            throw new BusinessException("The member is already assigned to this package.");


        // 4. Assign Package To Member
        member.AssignPackage(packageId);


        // 5. Save Changes
        await _uow.SaveChangesAsync(ct);
    }


    // Remove A Package From A Member
    // -------------------
    public async Task RemovePackageAsync(int memberId, CancellationToken ct = default)
    {
        // 1. Get Member By Id Or Throw NotFoundException If Not Found.
        var member = await GetMemberOrThrowAsync(memberId, ct);


        // 2. Ensure Member Has A Package Assigned
        if (!member.PackageId.HasValue)
            throw new BusinessException("The member does not have a package assigned.");


        // 3. Remove Package From Member
        member.RemovePackage();


        // 4. Save Changes
        await _uow.SaveChangesAsync(ct);
    }


    // Renew Membership For A Member
    // -------------------
    public Task RenewMembershipAsync(int memberId, RenewMembershipRequest request, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }


    // Expire Membership For A Member
    // -------------------
    public Task ExpireMembershipAsync(int memberId, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }


    // Freeze Membership For A Member
    // -------------------
    public Task FreezeMembershipAsync(int memberId, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }


    // Unfreeze Membership For A Member
    // -------------------
    public Task UnfreezeMembershipAsync(int memberId, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }





    // Private Helper Methods For Business Rule Validations Can Be Added Here
    // ===============================

    private async Task EnsureEmailIsUniqueAsync(string email, int? excludedMemberId, CancellationToken ct)
    {
        // "اتأكد مفيش حد تاني غيري عنده نفس القيمة"
        var exists = await _uow.Members.ExistsAsync(m => m.Email == email &&
                     (!excludedMemberId.HasValue || m.Id != excludedMemberId.Value),ct);

        if (exists)  throw new BusinessException("A member with this email already exists.");
    }

    private async Task EnsurePhoneIsUniqueAsync(string phone,int? excludedMemberId,CancellationToken ct)
    {
        // "اتأكد مفيش حد تاني غيري عنده نفس القيمة"
        var exists = await _uow.Members.ExistsAsync(m => m.Phone == phone &&
                 (!excludedMemberId.HasValue || m.Id != excludedMemberId.Value),ct);

        if (exists)   throw new BusinessException("A member with this phone number already exists.");
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

    private async Task<Member> GetMemberOrThrowAsync(int memberId,CancellationToken ct)
    {
        var member = await _uow.Members.GetByIdAsync(memberId, ct);

        if (member is null)
            throw new NotFoundException("Member was not found.");

        return member;
    }

    private async Task EnsureMemberHasNoBookingsAsync(int memberId,CancellationToken ct)
    {
        var hasBookings = await _uow.Bookings.ExistsAsync(b => b.MemberId == memberId,ct);

        if (hasBookings)
            throw new BusinessException("Cannot delete a member because they have existing bookings.");
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
