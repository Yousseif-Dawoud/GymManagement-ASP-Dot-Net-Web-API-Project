
using Gym.Application.DTOs.Members;

namespace Gym.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MembersController : ControllerBase
{
    // Inject the IMemberService into the controller
    private readonly IMemberService _memberService;
    public MembersController(IMemberService memberService) { _memberService = memberService; }




    [HttpPost]
    [ProducesResponseType(typeof(MemberResponse), StatusCodes.Status201Created)]
    public async Task<ActionResult<MemberResponse>> Create( [FromBody] CreateMemberRequest request, CancellationToken ct)
    {
        // 1.Call the service to create a new member
        var result = await _memberService.CreateAsync(request , ct);


        // 2.Return a 201 Created response with the location of the newly created member
        return CreatedAtAction(nameof(GetById),new { memberId = result.Id },result);
    }



    [HttpGet("{memberId:int}")]
    [ProducesResponseType(typeof(MemberResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<MemberResponse>> GetById( [FromRoute] int memberId, CancellationToken ct)
    {
        // 1.Call the service to get the member by ID
        var result = await _memberService.GetByIdAsync(memberId, ct);


        // 2.Return a 200 OK response with the member data
        return Ok(result);
    }

}