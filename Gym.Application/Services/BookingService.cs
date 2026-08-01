namespace Gym.Application.Services;

public sealed class BookingService : IBookingService
{
    // Inject the unit of work to manage database operations related to bookings.
    private readonly IUnitOfWork _uow;
    public BookingService(IUnitOfWork uow) => _uow = uow;
    
}
