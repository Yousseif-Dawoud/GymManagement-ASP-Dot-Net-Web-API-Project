
namespace Gym.Application.DTOs.Members;

public sealed record MemberResponse(
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
    string MembershipPlanName,

    int? PackageId,
    string? PackageName
);