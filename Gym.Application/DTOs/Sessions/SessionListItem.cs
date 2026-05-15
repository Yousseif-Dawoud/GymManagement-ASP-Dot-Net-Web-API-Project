
namespace Gym.Application.DTOs.Sessions;

public sealed record SessionListItem(
    int Id,

    string Name,

    SessionType Type,

    SessionStatus Status,

    DateTime StartTime,

    int Capacity,

    string TrainerName
);

