
namespace Gym.Application.DTOs.Sessions;

public sealed record UpdateSessionRequest(
    string Name,
    string? Description,
    SessionType Type,
    DateTime StartTime,
    DateTime EndTime,
    int Capacity,
    int TrainerId
);

