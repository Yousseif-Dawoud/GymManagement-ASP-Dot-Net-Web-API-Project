

namespace Gym.API.Controllers;

[ApiController]
[Route("api/[controller]")]

public sealed class MembersController : ControllerBase
{
    // Inject The MemberService To Handle Business Logic Related To Members
    private readonly IMemberService _memberService;
    public MembersController(IMemberService memberService) {  _memberService = memberService; }



    // This Endpoint Make Get All Members And Return A List Of MemberListItem DTOs
    // -----------------------------------------------------------------------------
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<MemberListItem>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<MemberListItem>>> GetAllMembers(CancellationToken ct = default)
    {
        var members = await _memberService.GetAllMembers(ct);
        return Ok(members);
    }


    // This Endpoint Make Get A Member By Id And Return A MemberDetails DTO
    // ---------------------------------------------------------------------
    [HttpGet("{id:int:min(1)}")]
    [ProducesResponseType(typeof(MemberResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<MemberResponse>> GetById(int id, CancellationToken ct = default)
    {
        var member = await _memberService.GetByIdAsync(id, ct);
        return Ok(member);
    }
}