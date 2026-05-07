
namespace Gym.Application.Interfaces.IServices;

public interface IMemberService
{
    // 1.Create Member : This method allows you to create a new member in the gym. 
    Task<MemberResponse> CreateAsync(CreateMemberRequest request, CancellationToken ct = default);



    // 2.Get all Members : This method retrieves a list of all members in the gym.
    Task<IReadOnlyList<MemberListItem>> GetAllMembers(CancellationToken ct = default);



    // 3.Get Member By Id : This method retrieves the details of a specific member based on their unique identifier (Id).
    Task<MemberResponse> GetByIdAsync(int id, CancellationToken ct = default);



    // 4.Update Member : This method allows you to update the details of an existing member .
    Task<MemberResponse> UpdateAsync(int id, UpdateMemberRequest request, CancellationToken ct = default);



    Task<MemberResponse> GetProfileAsync(int id, CancellationToken ct = default);

}