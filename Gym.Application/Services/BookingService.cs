
namespace Gym.Application.Services;

public sealed class BookingService : IBookingService
{
    // Inject the unit of work to manage database operations related to bookings.
    private readonly IUnitOfWork _uow;
    public BookingService(IUnitOfWork uow) => _uow = uow;



    // Implement the method to create a new booking based on the incoming request data.
    // -----------------------------------------------------------------------------------------------
    public async Task<BookingResponse> CreateBookingAsync(CreateBookingRequest request, CancellationToken ct = default)
    {
        // 1. Validate the incoming request data ( check if MemberId are valid).
        var member = await _uow.Members.GetByIdAsync(request.MemberId, ct);
        if(member is null)
            throw new NotFoundException($"Member with id {request.MemberId} was not found.");



        // 2. Validate the incoming request data ( check if SessionId are valid).
        var session = await _uow.Sessions.GetByIdAsync(request.SessionId, ct);
        if (session is null)
            throw new NotFoundException($"Session with id {request.SessionId} was not found.");



        // 3. Check if the session has available capacity for new bookings.
        var allBookings = await _uow.Bookings.GetAllAsync(ct);

        var sessionBookings = allBookings.Where(b => b.SessionId == request.SessionId).ToList();
        if (sessionBookings.Count >= session.Capacity)
            throw new BusinessRuleException("Session is full.");



        // 4. Check if the member has already booked the same session to prevent duplicate bookings.
        if (sessionBookings.Any(b => b.MemberId == request.MemberId))
            throw new BusinessRuleException("Member already booked this session.");



        // 5. If all validations pass, create a new booking entity and save it to the database.
        var booking = new Booking(request.MemberId, request.SessionId);



        // 6. Save the new booking to the database using the unit of work pattern.
        await _uow.Bookings.AddAsync(booking, ct);



        // 7. after saving the booking, check if the session has reached its capacity
        // and update the session status As full if necessary. 
        if (sessionBookings.Count + 1 >= session.Capacity)
            session.MarkAsFull();



        // 8. Commit the transaction to persist the changes to the database.
        await _uow.SaveChangesAsync(ct);



        // 9. Map the created booking entity to a response DTO and return it to the caller.
        return new BookingResponse(
                Id: booking.Id,
                MemberId: booking.MemberId,
                MemberName: member.FullName,
                SessionId: booking.SessionId,
                SessionTitle: session.Title,
                BookingDate: booking.BookingDate,
                CreatedAt: booking.CreatedAt,
                UpdatedAt: booking.UpdatedAt
        );
    }
    // -----------------------------------------------------------------------------------------------




    // Implement the method to cancel an existing booking based on the provided booking ID.
    // -----------------------------------------------------------------------------------------------
    public async Task CancelAsync(int bookingId, CancellationToken ct = default)
    {
        // 1. Retrieve the booking entity from the database using the provided booking ID.
        var booking = await _uow.Bookings.GetByIdAsync(bookingId, ct);



        // 2. If the booking does not exist, throw a NotFoundException 
        if (booking is null)
            throw new NotFoundException($"Booking with id {bookingId} was not found.");



        // 3. If the booking exists, delete it from the database using the unit of work pattern.
         _uow.Bookings.Remove(booking);



        // 4. Commit the transaction to persist the changes to the database.
        await _uow.SaveChangesAsync(ct);
    }
    // -----------------------------------------------------------------------------------------------
}
