namespace Gym.Application.DTOs.Trainers;

using System.ComponentModel.DataAnnotations;

public sealed record CreateTrainerRequest(
    [param: Required, MaxLength(120)] string FullName,
    [param: Required, MaxLength(120)] string Specialty
);
