
namespace Gym.Application.Services;

public sealed class TrainerService : ITrainerService
{
    // Inject the unit of work to manage database operations related to trainers. 
    private readonly IUnitOfWork _uow;
    public TrainerService(IUnitOfWork uow) => _uow = uow;

 
}
