
namespace Gym.Application.DTOs.Members;

public sealed record MemberListItem(
     int Id,
    string FullName,
    string Phone,

    MembershipStatus Status,

    MembershipPlanType MembershipPlanType,

    string? PackageName,

    DateOnly MembershipEndDate
);

