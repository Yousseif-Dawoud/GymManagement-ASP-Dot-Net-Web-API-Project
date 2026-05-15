namespace Gym.Application.DTOs.Trainers;

public sealed record TrainerProfileResponse(
    int Id,

    string FullName,

    string Specialization,

    int ExperienceYears,

    TrainerStatus Status
);