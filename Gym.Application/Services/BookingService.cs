
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




    // Implement the method to retrieve all bookings and map them to a list of booking list items.
    public async Task<IReadOnlyList<BookingListItem>> GetAllBookingAsync(CancellationToken ct = default) 
    {
        // 1. Retrieve all booking entities from the database using the unit of work pattern.
        var bookings = await _uow.Bookings.GetAllAsync(ct);


        // 2. Retrieve all members to map the booking entities to the corresponding member 
        var members = await _uow.Members.GetAllAsync(ct);
        if (members is null)
            throw new BusinessRuleException("No members found.");

        // 3. Retrieve all sessions to map the booking entities to the corresponding session
        var sessions = await _uow.Sessions.GetAllAsync(ct);
        if(sessions is null)
            throw new BusinessRuleException("No sessions found.");


        // 4. Map the booking entities to a list of booking list items,including member names and session titles.
        var memberNames = members.ToDictionary(m => m.Id, m => m.FullName);
        var sessionTitles = sessions.ToDictionary(s => s.Id, s => s.Title);


        // 5. Return the list of booking list items to the caller.
        return bookings.Select(b => new BookingListItem(
           Id: b.Id,
           MemberId: b.MemberId,
           MemberName: memberNames.TryGetValue(b.MemberId, out var mn) ? mn : string.Empty,
           SessionId: b.SessionId,
           SessionTitle: sessionTitles.TryGetValue(b.SessionId, out var st) ? st : string.Empty,
           BookingDate: b.BookingDate
       )).ToList();
    }
    // -----------------------------------------------------------------------------------------------
}
