
namespace Gym.Application.Services;

public sealed class SessionService : ISessionService
{

    // Inject the unit of work to manage database operations related to sessions.
    private readonly IUnitOfWork _uow;
    public SessionService(IUnitOfWork uow) => _uow = uow;




    // Implement the CreateSessionAsync method to create a new session based on the provided request data.
    // -----------------------------------------------------------------------------------------------
    public async Task<SessionResponse> CreateSessionAsync(CreateSessionRequest request, CancellationToken ct = default)
    {
        // 1. Retrieve the trainer from the database using the provided TrainerId in the request.
        var trainer = await _uow.Trainers.GetByIdAsync(request.TrainerId, ct);


        // 2. If the trainer does not exist, throw a NotFoundException to indicate that the specified trainer was not found.
        if (trainer is null)
            throw new NotFoundException($"Trainer with id {request.TrainerId} was not found.");


        // 3. Create a new Session entity using the data from the request and the retrieved trainer.
        var session = new Session(
           title: request.Title,
           date: request.Date,
           startTime: request.StartTime,
           capacity: request.Capacity,
           trainerId: request.TrainerId
       );


        // 4. Add the new created session to the database using the unit of work's Sessions repository.
        await _uow.Sessions.AddAsync(session, ct);


        // 5. Commit the changes to the database to persist the new session.
        await _uow.SaveChangesAsync(ct);


        // 6. Map the created session entity to a SessionResponse DTO and return it as the result of the method.
        return MapToResponse(session, trainer.FullName);
    }
    // -----------------------------------------------------------------------------------------------




    // Implement the GetSessionByIdAsync method to retrieve a session by its ID and return a SessionResponse DTO.
    // -----------------------------------------------------------------------------------------------
    public async Task<SessionResponse> GetSessionByIdAsync(int id, CancellationToken ct = default)
    {
        // 1. Retrieve the session from the database using the provided sessionId.
        var session = await _uow.Sessions.GetByIdAsync(id, ct);


        // 2. If the session does not exist, throw a NotFoundException 
        if (session is null)
            throw new NotFoundException($"Session with id {id} was not found.");


        // 3. Retrieve the trainer associated with the session to include the trainer's name in the response.
        var trainer = await _uow.Trainers.GetByIdAsync(session.TrainerId, ct);
        var trainerName = trainer?.FullName ?? "Unknown Trainer";


        // 4. Map the session entity to a SessionResponse DTO, including the trainer's name.
        return MapToResponse(session, trainerName);
    }
    // -----------------------------------------------------------------------------------------------




    // Implement the GetAllSessionsAsync method to retrieve all sessions and return a list of SessionResponse DTOs.
    // -----------------------------------------------------------------------------------------------
    public async Task<IReadOnlyList<SessionListItem>> GetAllSessionsAsync(CancellationToken ct = default)
    {
        // 1. Retrieve all sessions from the database.
        var sessions = await _uow.Sessions.GetAllAsync(ct);


        // 2. Map each session entity to a SessionListItem DTO.
        return sessions
           .Select(s => new SessionListItem(
               Id: s.Id,
               Title: s.Title,
               Date: s.Date,
               StartTime: s.StartTime,
               Capacity: s.Capacity,
               Status: s.Status.ToString()
           ))
           .ToList();
    }
    // -----------------------------------------------------------------------------------------------




    // A private helper method to map a Session entity to a SessionResponse DTO, including the trainer's name.
    private static SessionResponse MapToResponse(Session session, string trainerName)
        => new(
            Id: session.Id,
            Title: session.Title,
            Date: session.Date,
            StartTime: session.StartTime,
            Capacity: session.Capacity,
            TrainerId: session.TrainerId,
            TrainerName: trainerName,
            Status: session.Status.ToString(),
            CreatedAt: session.CreatedAt,
            UpdatedAt: session.UpdatedAt
        );
}