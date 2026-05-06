
namespace Gym.Application.Interfaces.IServices;

public interface IMemberService
{
    // 1.Get all Members : This method retrieves a list of all members in the gym.
    Task<IReadOnlyList<MemberListItem>> GetAllMembers(CancellationToken ct = default);


    // 2.Get Member By Id : This method retrieves the details of a specific member based on their unique identifier (Id).

    Task<MemberResponse> GetByIdAsync(int id, CancellationToken ct = default);










    // 1. Create Member
    // 4. Get Profile

    Task<MemberResponse> CreateAsync(CreateMemberRequest request, CancellationToken ct = default);

    //Task<IReadOnlyList<MemberListItem>> GetAllMembers(CancellationToken ct = default);

    //Task<MemberResponse> GetByIdAsync(int id, CancellationToken ct = default);

    Task<MemberResponse> GetProfileAsync(int id, CancellationToken ct = default);

}