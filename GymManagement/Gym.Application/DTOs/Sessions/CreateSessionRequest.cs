namespace Gym.Application.DTOs.Sessions;

using System.ComponentModel.DataAnnotations;

public sealed record CreateSessionRequest(
    [param: Required, MaxLength(120)] string Title,
    DateOnly Date,
    TimeOnly StartTime,
    [param: Range(1, 200)] int Capacity,
    [param: Range(1, int.MaxValue)] int TrainerId
);
