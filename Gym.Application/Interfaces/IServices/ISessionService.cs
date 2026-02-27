
namespace Gym.Application.Interfaces.IServices;

public interface ISessionService
{
    // The session service interface defines methods for managing sessions in the gym application.

    // 1. CreateSession
    Task<SessionResponse> CreateSessionAsync(CreateSessionRequest request , CancellationToken ct = default);



    // 2. GetSessionByIdAsync
    Task<SessionResponse> GetSessionByIdAsync(int id, CancellationToken ct = default);



    // 3. GetAllSessionsAsync
    Task<IReadOnlyList<SessionListItem>> GetAllSessionsAsync(CancellationToken ct = default);

}
