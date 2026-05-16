namespace Gym.Application.DTOs.Members;

public sealed record MemberQueryRequest(
    string? SearchTerm,
    MembershipStatus? Status,
    int? MembershipPlanId,
    int PageNumber = 1,
    int PageSize = 10
);