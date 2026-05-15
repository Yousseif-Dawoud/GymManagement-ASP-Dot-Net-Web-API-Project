namespace Gym.Application.DTOs.Sessions;

public sealed record SessionScheduleResponse(
    int Id,
    string Name,
    DateTime StartTime,
    DateTime EndTime,
    string TrainerName
);