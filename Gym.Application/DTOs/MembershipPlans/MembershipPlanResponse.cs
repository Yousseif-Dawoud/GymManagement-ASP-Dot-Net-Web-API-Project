
namespace Gym.Application.DTOs.MembershipPlans;

public sealed record MembershipPlanResponse(
    int Id,
    string Type,
    decimal Price,
    string Description,
    int DurationInDays,
    int MaxSessionsPerMonth,
    bool IncludesPersonalTrainer,
    bool IsActive
);
