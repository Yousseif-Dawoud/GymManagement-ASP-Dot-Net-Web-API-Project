namespace Gym.Application.DTOs.Members;

public sealed record MemberProfileResponse(

   int Id,

    string FullName,
    string Phone,
    string Email,

    Gender Gender,
    DateOnly DateOfBirth,

    string? EmergencyContact,

    MembershipStatus Status,

    DateOnly MembershipStartDate,
    DateOnly MembershipEndDate,

    int MembershipPlanId,
    MembershipPlanType MembershipPlanType,

    int? PackageId,
    string? PackageName,

    bool HasPersonalTrainer

);