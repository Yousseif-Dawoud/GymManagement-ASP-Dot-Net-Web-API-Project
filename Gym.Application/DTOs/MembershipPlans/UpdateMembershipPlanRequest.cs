namespace Gym.Application.DTOs.MembershipPlans;

public sealed record UpdateMembershipPlanRequest(
    string Name,
    MembershipPlanType Type,
    decimal Price,
    string Description,
    int MaxSessionsPerMonth,
    bool IncludesPersonalTrainer
);
