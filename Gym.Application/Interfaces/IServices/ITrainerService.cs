


namespace Gym.Application.Interfaces.IServices;

public interface ITrainerService
{
    // The trainer service interface defines methods for managing trainers in the gym application.



    // 1. CreateTrainerAsync
    Task<TrainerResponse> CreateTrainerAsync(CreateTrainerRequest request , CancellationToken ct = default);



    // 2. GetTrainerByIdAsync
    Task<TrainerResponse> GetTrainerByIdAsync(int id, CancellationToken ct = default);



    // 3. GetAllTrainersAsync
    Task<IReadOnlyList<TrainerListItem>> GetAllTrainersAsync(CancellationToken ct = default);
}
