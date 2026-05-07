namespace Gym.Application.DTOs.Members;

public sealed record MemberProfileResponse(

    int Id,
    string FullName,
    string Phone,
    string Email,
    DateOnly MembershipStartDate

);