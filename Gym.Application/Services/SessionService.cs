
namespace Gym.Application.Services;

public sealed class SessionService : ISessionService
{

    // Inject the unit of work to manage database operations related to sessions.
    private readonly IUnitOfWork _uow;
    public SessionService(IUnitOfWork uow) => _uow = uow;


}