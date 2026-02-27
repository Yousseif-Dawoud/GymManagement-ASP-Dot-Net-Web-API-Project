
using Gym.Application.DTOs.MembershipPlans;

namespace Gym.Application.Interfaces.IServices;

public interface IMembershipPlanService
{
    // The membership plan service interface defines methods for managing membership plans in the gym application.



    // 1. CreateMembershipPlan
    Task<MembershipPlanResponse> CreateMembershipPlanAsync(CreateMembershipPlanRequest request, CancellationToken ct = default);



    // 2. GetMembershipPlanById
    Task<MembershipPlanResponse> GetMembershipPlanByIdAsync(int id, CancellationToken ct = default);



    // 3. GetAllMembershipPlans
    Task<IReadOnlyList<MembershipPlanListItem>> GetAllMembershipPlans(CancellationToken ct = default);
}