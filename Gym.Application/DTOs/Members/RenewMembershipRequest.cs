namespace Gym.Application.DTOs.Members;

public sealed record RenewMembershipRequest(
    int MembershipPlanId,
    DateOnly StartDate,
    DateOnly EndDate
);