
namespace Gym.Application.Services;
public sealed class MembershipPlanService : IMembershipPlanService
{
    // Inject the unit of work to manage database operations related to membership plans.
    private readonly IUnitOfWork _uow;
    public MembershipPlanService(IUnitOfWork uow) => _uow = uow;
    
}