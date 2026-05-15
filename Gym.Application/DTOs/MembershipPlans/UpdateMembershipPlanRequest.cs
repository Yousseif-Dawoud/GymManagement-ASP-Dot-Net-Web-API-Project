namespace Gym.Application.DTOs.MembershipPlans;

public sealed record UpdateMembershipPlanRequest(
    MembershipPlanType Type,
    decimal Price,
    string Description,
    int DurationInDays,
    int MaxSessionsPerMonth,
    bool IncludesPersonalTrainer
);
