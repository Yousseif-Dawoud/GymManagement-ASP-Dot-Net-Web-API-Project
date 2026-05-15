
namespace Gym.Application.DTOs.MembershipPlans;

public sealed record CreateMembershipPlanRequest(
    MembershipPlanType Type,
    decimal Price,
    string Description,
    int DurationInDays,
    int MaxSessionsPerMonth,
    bool IncludesPersonalTrainer
);
