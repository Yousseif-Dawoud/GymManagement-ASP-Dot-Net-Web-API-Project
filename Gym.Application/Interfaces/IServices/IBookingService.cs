
using Gym.Application.DTOs.Bookings;

namespace Gym.Application.Interfaces.IServices;

public interface IBookingService
{
    // The booking service interface defines methods for managing bookings in the gym application.


    // 1. CreateBooking
    Task<BookingResponse> CreateBookingAsync(CreateBookingRequest request, CancellationToken ct = default);

    // 2. CancelBooking
    // 3. GetAllBooking
}