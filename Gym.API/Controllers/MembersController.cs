
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
    public async Task<ActionResult<MemberResponse>> Create([FromBody] CreateMemberRequest request, CancellationToken ct)
    {
        // 1.Call the service to create a new member
        var result = await _memberService.CreateAsync(request, ct);


        // 2.Return a 201 Created response with the location of the newly created member
        return CreatedAtAction(nameof(GetById), new { memberId = result.Id }, result);
    }



    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<MemberListItem>), StatusCodes.Status200OK)]
    public async Task<ActionResult<PagedResult<MemberListItem>>> GetAll([FromQuery] MemberQueryRequest request, CancellationToken ct)
    {
        // 1.Call the service to search for members based on the query request
        var result = await _memberService.SearchAsync(request, ct);


        // 2.Return a 200 OK response with the paged result of members
        return Ok(result);
    }



    //[HttpGet("{memberId:int}")] // If User Input a invalid memberId as intger But This Number is a Nigative .
    [HttpGet("{memberId:int:min(1)}")] 
    [ProducesResponseType(typeof(MemberResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<MemberResponse>> GetById([FromRoute] int memberId, CancellationToken ct)
    {
        // 1.Call the service to get the member by ID
        var result = await _memberService.GetByIdAsync(memberId, ct);


        // 2.Return a 200 OK response with the member data
        return Ok(result);
    }



    [HttpGet("{memberId:int:min(1)}/profile")]
    [ProducesResponseType(typeof(MemberProfileResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MemberProfileResponse>> GetProfile([FromRoute] int memberId, CancellationToken ct)
    {
        // 1.Call the service to get the member profile by ID
        var result = await _memberService.GetProfileAsync(memberId, ct);


        // 2.Return a 200 OK response with the member profile data
        return Ok(result);
    }



    [HttpPut("{memberId:int:min(1)}")]
    [ProducesResponseType(typeof(MemberResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<MemberResponse>> Update([FromRoute] int memberId, [FromBody] UpdateMemberRequest request, CancellationToken ct)
    {
        // 1.Call the service to update the member with the specified ID
        var result = await _memberService.UpdateAsync(memberId, request, ct);


        // 2.Return a 200 OK response with the updated member data
        return Ok(result);
    }



    [HttpDelete("{memberId:int:min(1)}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] int memberId, CancellationToken ct)
    {
        // 1.Call the service to delete the member with the specified ID
        await _memberService.DeleteAsync(memberId, ct);


        // 2.Return a 204 No Content response
        return NoContent();
    }



    [HttpPut("{memberId:int:min(1)}/package")]
    [ProducesResponseType(typeof(MemberResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<MemberResponse>> AssignPackage([FromRoute] int memberId, [FromBody] AssignPackageRequest request, CancellationToken ct)
    {
        // 1.Call the service to assign a package to the member with the specified ID
        var result = await _memberService.AssignPackageAsync(memberId, request.PackageId, ct);


        // 2.Return a 200 OK response with the updated member data
        return Ok(result);
    }



    [HttpDelete("{memberId:int:min(1)}/package")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RemovePackage([FromRoute] int memberId, CancellationToken ct)
    {
        // 1.// Remove the assigned package
        await _memberService.RemovePackageAsync(memberId, ct);


        // 2.Return a 204 No Content response
        return NoContent();
    }



    [HttpPut("{memberId:int:min(1)}/membership")]
    [ProducesResponseType(typeof(MemberResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<MemberResponse>> RenewMembership([FromRoute] int memberId, [FromBody] RenewMembershipRequest request, CancellationToken ct)
    {
        // 1.Call the service to renew the member's membership
        var result = await _memberService.RenewMembershipAsync(memberId, request, ct);


        // 2.Return a 200 OK response with the updated member data
        return Ok(result);
    }



    [HttpPut("{memberId:int:min(1)}/membership/freeze")]
    [ProducesResponseType(typeof(MemberResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MemberResponse>> FreezeMembership([FromRoute] int memberId, CancellationToken ct)
    {
        // 1.Call the service to freeze the member's membership
        var result = await _memberService.FreezeMembershipAsync(memberId, ct);


        // 2.Return updated member
        return Ok(result);
    }



    [HttpPut("{memberId:int:min(1)}/membership/unfreeze")]
    [ProducesResponseType(typeof(MemberResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MemberResponse>> UnfreezeMembership([FromRoute] int memberId, CancellationToken ct)
    {
        // 1.Call the service to unfreeze the member's membership
        var result = await _memberService.UnfreezeMembershipAsync(memberId, ct);


        // 2.Return updated member
        return Ok(result);
    }



    [HttpPut("{memberId:int:min(1)}/membership/expire")]
    [ProducesResponseType(typeof(MemberResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MemberResponse>> ExpireMembership([FromRoute] int memberId, CancellationToken ct)
    {
        // 1.Call the service to expire the member's membership
        var result = await _memberService.ExpireMembershipAsync(memberId, ct);


        // 2.Return updated member
        return Ok(result);
    }
}