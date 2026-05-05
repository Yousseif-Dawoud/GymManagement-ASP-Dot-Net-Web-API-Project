namespace Gym.Application.DTOs.Bookings;

using System.ComponentModel.DataAnnotations;

public sealed record CreateBookingRequest(
    [param: Range(1, int.MaxValue)] int MemberId,
    [param: Range(1, int.MaxValue)] int SessionId
);
