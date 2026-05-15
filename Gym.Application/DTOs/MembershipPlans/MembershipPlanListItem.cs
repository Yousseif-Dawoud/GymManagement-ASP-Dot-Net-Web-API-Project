
namespace Gym.Application.DTOs.MembershipPlans;

public sealed record MembershipPlanListItem(
    int Id,
    string Type,
    decimal Price,
    bool IsActive
);

