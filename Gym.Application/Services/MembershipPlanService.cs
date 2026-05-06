
namespace Gym.Application.Services;

public sealed class MembershipPlanService : IMembershipPlanService
{
    // Inject the unit of work to manage database operations related to membership plans.
    private readonly IUnitOfWork _uow;
    public MembershipPlanService(IUnitOfWork uow) => _uow = uow;



    // Implement the Create Membership Plan Async method to create a new membership.
    // -----------------------------------------------------------------------------------------------
    public async Task<MembershipPlanResponse> CreateMembershipPlanAsync(CreateMembershipPlanRequest request, CancellationToken ct = default)
    {
        // 1. create a new MembershipPlan entity using the data from the request.
        var plan = new MembershipPlan(
            name: request.Name,
            price: request.Price,
            maxSessionsPerMonth: request.MaxSessionsPerMonth
        );


        // 2. add the new plan to the repository and save changes to the database.
        await _uow.MembershipPlans.AddAsync(plan, ct);
        await _uow.SaveChangesAsync(ct);


        // 3. return a MembershipPlanResponse with the details of the created plan.
        return MapToResponse(plan);
    }



    // Implement the GetMembershipPlanByIdAsync method to retrieve a membership plan by its ID.
    // -----------------------------------------------------------------------------------------------
    public async Task<MembershipPlanResponse> GetMembershipPlanByIdAsync(int id, CancellationToken ct = default)
    {
        // 1. retrieve the membership plan from the repository using the provided ID.
        var plan = await _uow.MembershipPlans.GetByIdAsync(id, ct);


        // 2. if the plan is not found, throw a NotFoundException.
        if (plan is null)
            throw new NotFoundException($"Membership plan with ID {id} not found.");


        // 3. if the plan is found, return a MembershipPlanResponse with the details of the plan.
        return MapToResponse(plan);
    }




    //// Implement the GetAllMembershipPlans method to retrieve all membership plans.
    //// -----------------------------------------------------------------------------------------------
    //public async Task<IReadOnlyList<MembershipPlanListItem>> GetAllMembershipPlans(CancellationToken ct = default)
    //{
    //    // 1. retrieve all membership plans from the repository.
    //    var plans = await _uow.MembershipPlans.GetAllAsync(ct);


    //    // 2. map the retrieved plans to a list of MembershipPlanListItem and return it.
    //    return plans
    //         .Select(p => new MembershipPlanListItem(
    //             Id: p.Id,
    //             Name: p.Name,
    //             Price: p.Price,
    //             MaxSessionsPerMonth: p.MaxSessionsPerMonth
    //         ))
    //         .ToList();
    //}
    //// -----------------------------------------------------------------------------------------------





    // Helper method to map a MembershipPlan entity to a MembershipPlanResponse DTO.
    private static MembershipPlanResponse MapToResponse(MembershipPlan plan)
        => new(
            Id: plan.Id,
            Name: plan.Name,
            Price: plan.Price,
            MaxSessionsPerMonth: plan.MaxSessionsPerMonth,
            CreatedAt: plan.CreatedAt,
            UpdatedAt: plan.UpdatedAt
        );
}