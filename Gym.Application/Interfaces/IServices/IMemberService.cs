
using Gym.Application.Common;

namespace Gym.Application.Interfaces.IServices;

// This Interface Make Definition For All Use Cases Related To Member Entity .

// Read Operations For Member Entity :     
//  - Get All Members => Specification Repository .
//  - Get Member By Id
//  - Search Members

// Write Operations For Member Entity :
//  - Create Member
//  - Update Member
//  - Delete Member

// MemberShip Use Case Operations For Member  :
//  - Renew Membership
//  - Freeze Membership
//  - Unfreeze Membership
//  - Expire Membership

// Package Operations For Member :
//  - Assign Package
//  - Remove Package

public interface IMemberService
{
    // Read Operations And Queries For Member Entity : Why i make it comment ? Because i will Apply Specification Pattern For Search Operation And Get All Operation To Make It More Flexible And Reusable For Any Entity In The Future So I Will Implement It Later When I Implement Specification Pattern For The Project .
    // -----------------------------------
    //Task<PagedResult<MemberListItem>> SearchAsync(MemberQueryRequest request,CancellationToken ct = default); // GetAll 

    Task<MemberResponse> GetByIdAsync(int memberId,CancellationToken ct = default);

    Task<MemberProfileResponse> GetProfileAsync(int memberId,CancellationToken ct = default);



    // Write Operations And Commands For Member Entity :
    // -----------------------------------
    Task<MemberResponse> CreateAsync(CreateMemberRequest request,CancellationToken ct = default);

    Task<MemberResponse> UpdateAsync(int memberId,UpdateMemberRequest request,CancellationToken ct = default);

    Task DeleteAsync(int memberId,CancellationToken ct = default);



    // Membership Use Case Operations For Member  :
    Task RenewMembershipAsync(int memberId,RenewMembershipRequest request,CancellationToken ct = default);

    Task FreezeMembershipAsync( int memberId,CancellationToken ct = default);

    Task UnfreezeMembershipAsync(int memberId,CancellationToken ct = default);

    Task ExpireMembershipAsync(int memberId,CancellationToken ct = default);



    // Package Operations For Member :
    Task AssignPackageAsync(int memberId,int packageId,CancellationToken ct = default);

    Task RemovePackageAsync(int memberId,CancellationToken ct = default);

}