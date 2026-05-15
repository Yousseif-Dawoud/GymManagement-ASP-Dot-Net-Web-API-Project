namespace Gym.Application.DTOs.Bookings;

public sealed record BookingDetailsResponse(
    int Id,

    int MemberId,
    string MemberName,
    string Phone,

    int SessionId,
    string SessionName,
    DateTime StartTime,
    DateTime EndTime,

    BookingStatus Status,
    DateTime BookingDate,

    string? Notes
);