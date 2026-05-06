using Gym.Application.DTOs.MembershipPlans;

namespace Gym.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MembershipPlansController : ControllerBase
{
    // Inject The MembershipPlanService To Handle Business Logic Related To Membership Plans
    private readonly IMembershipPlanService _membershipPlanService;
    public MembershipPlansController(IMembershipPlanService membershipPlanService)
        => _membershipPlanService = membershipPlanService;



    // This Endpoint Make Create A New Membership Plan And Return The Created MembershipPlan Details DTO
    // ---------------------------------------------------------------------------------------------------
    [HttpPost("Create-Membership-Plan")]
    [ProducesResponseType(typeof(MembershipPlanResponse), StatusCodes.Status201Created)]
    public async Task<ActionResult<MembershipPlanResponse>> CreateMembershipPlan([FromBody] CreateMembershipPlanRequest request, CancellationToken ct = default)
    {
        var plan = await _membershipPlanService.CreateMembershipPlanAsync(request, ct);
        return CreatedAtAction(nameof(GetById), new { id = plan.Id }, plan);
    }



    [HttpGet("{id}")]
    [ProducesResponseType(typeof(MembershipPlanResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<MembershipPlanResponse>> GetById(int id, CancellationToken ct = default)
    {
        var plan = await _membershipPlanService.GetMembershipPlanByIdAsync(id, ct);
        return Ok(plan);
    }
}