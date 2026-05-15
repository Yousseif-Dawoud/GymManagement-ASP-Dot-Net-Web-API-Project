
namespace Gym.Application.DTOs.Trainers;

public sealed record TrainerResponse(
    int Id,

    string FullName,
    string Phone,
    string Email,

    Gender Gender,

    DateOnly DateOfBirth,

    string Specialization,

    int ExperienceYears,

    DateOnly HireDate,

    string? Bio,

    TrainerStatus Status
);
