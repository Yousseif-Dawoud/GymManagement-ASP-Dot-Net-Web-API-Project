
namespace Gym.Application.DTOs.Bookings;

public sealed record BookingResponse(
    int Id,

    int MemberId,
    string MemberName,

    int SessionId,
    string SessionName,

    DateTime BookingDate,

    BookingStatus Status,

    string? Notes
);

