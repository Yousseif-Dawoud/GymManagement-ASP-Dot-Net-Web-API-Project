namespace Gym.Application.DTOs.Bookings;

public sealed record UpdateBookingRequest(
    int Id,
    string? Notes
);