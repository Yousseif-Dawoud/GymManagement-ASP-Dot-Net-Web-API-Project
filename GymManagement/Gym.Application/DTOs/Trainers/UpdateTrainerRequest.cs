namespace Gym.Application.DTOs.Trainers;

using System.ComponentModel.DataAnnotations;

public sealed record UpdateTrainerRequest(
    [param: Required, MaxLength(120)] string FullName,
    [param: Required, MaxLength(120)] string Specialty
);
