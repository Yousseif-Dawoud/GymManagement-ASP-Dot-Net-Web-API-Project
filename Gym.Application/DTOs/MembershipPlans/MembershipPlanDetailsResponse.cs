namespace Gym.Application.DTOs.MembershipPlans;

public sealed record MembershipPlanDetailsResponse(
    int Id,
    string Type,
    decimal Price,
    string Description,
    int DurationInDays,
    int MaxSessionsPerMonth,
    bool IncludesPersonalTrainer,
    bool IsActive,

    int MembersCount,
    int PackagesCount
);