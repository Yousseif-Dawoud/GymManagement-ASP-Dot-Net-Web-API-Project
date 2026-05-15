
namespace Gym.Application.DTOs.Members;

public sealed record CreateMemberRequest(
    string FullName,
    string Phone,
    string Email,
    Gender Gender,
    DateOnly DateOfBirth,
    string? EmergencyContact,
    DateOnly MembershipStartDate,
    DateOnly MembershipEndDate,
    int MembershipPlanId,
    int? PackageId
);
