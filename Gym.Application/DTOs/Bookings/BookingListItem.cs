
namespace Gym.Application.DTOs.Bookings;

public sealed record BookingListItem(
     int Id,

    string MemberName,
    string SessionName,

    BookingStatus Status
);