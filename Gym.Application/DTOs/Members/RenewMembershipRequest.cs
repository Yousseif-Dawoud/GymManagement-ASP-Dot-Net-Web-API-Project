namespace Gym.Application.DTOs.Members;

public sealed record RenewMembershipRequest(
     DateOnly StartDate,
     DateOnly EndDate,
     int? MembershipPlanId
 );