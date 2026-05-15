
namespace Gym.Application.DTOs.Sessions;

public sealed record SessionResponse(
     int Id,

    string Name,
    string? Description,

    SessionType Type,
    SessionStatus Status,

    DateTime StartTime,
    DateTime EndTime,

    int Capacity,

    int TrainerId,
    string TrainerName,

    int BookingsCount,

    DateTime CreatedAt,
    DateTime? UpdatedAt
);

