
namespace Gym.Application.DTOs.MembershipPlans;

public sealed record MembershipPlanResponse(
    int Id,
    string Name,
    string Type,
    decimal Price,
    string Description,
    int MaxSessionsPerMonth,
    bool IncludesPersonalTrainer,
    bool IsActive,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);
