
namespace Gym.Application.Interfaces.IServices;

public interface IMemberService
{
    // The member service interface defines methods for managing members in the gym application.

    // 1. Create Member
    // 2. Get All Members 
    // 3. Get Member By Id
    // 4. Get Profile

    Task<MemberResponse> CreateAsync(CreateMemberRequest request, CancellationToken ct = default);

    Task<IReadOnlyList<MemberListItem>> GetAllMembers(CancellationToken ct = default);

    Task<MemberResponse> GetByIdAsync(int id, CancellationToken ct = default);

    Task<MemberResponse> GetProfileAsync(int id, CancellationToken ct = default);

}