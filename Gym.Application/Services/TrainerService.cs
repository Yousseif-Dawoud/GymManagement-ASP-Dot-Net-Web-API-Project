
namespace Gym.Application.Services;

public sealed class TrainerService : ITrainerService
{
    // Inject the unit of work to manage database operations related to trainers. 
    private readonly IUnitOfWork _uow;
    public TrainerService(IUnitOfWork uow) => _uow = uow;




    // Implement the method to create a new trainer Based on the CreateTrainerRequest
    // -----------------------------------------------------------------------------------------------
    public async Task<TrainerResponse> CreateTrainerAsync(CreateTrainerRequest request, CancellationToken ct = default)
    {
        // 1.Create a new Trainer entity using the data from the request
        var trainer = new Trainer(
            request.FullName,
            request.Specialty 
            );


        // 2.Add the new trainer to the repository and save changes to the database
        await _uow.Trainers.AddAsync(trainer, ct);


        // 3.Commit the transaction to persist the new trainer in the database
        await _uow.SaveChangesAsync(ct);


        // 4.Map the created trainer entity to a TrainerResponse DTO and return it
        return MapToResponse(trainer);
    }
    // -----------------------------------------------------------------------------------------------




    // Implement the method to retrieve a trainer by their ID.
    // ------------------------------------------------------------------------------------------------
    public async Task<TrainerResponse> GetTrainerByIdAsync(int id, CancellationToken ct = default)
    {
        // 1.Retrieve the trainer entity from the repository using the provided ID
        var trainer = await _uow.Trainers.GetByIdAsync(id, ct);


        // 2.If the trainer is not found, throw a NotFoundException to indicate that the requested resource does not exist
        if (trainer is null)
            throw new NotFoundException($"Trainer with ID {id} not found.");


        // 3.Map the retrieved trainer entity to a TrainerResponse DTO and return it
        return MapToResponse(trainer);
    }
    // ------------------------------------------------------------------------------------------------




    // Implement the method to retrieve all trainers from the database.
    // ------------------------------------------------------------------------------------------------
    public async Task<IReadOnlyList<TrainerListItem>> GetAllTrainersAsync(CancellationToken ct = default)
    {
        // 1.Retrieve all trainer entities from the repository
        var trainers = await _uow.Trainers.GetAllAsync(ct);

        // 2.Map each trainer entity to a TrainerListItem DTO and return the list of trainers
        return trainers
           .Select(t => new TrainerListItem(
               Id: t.Id,
               FullName: t.FullName,
               Specialty: t.Specialty
           ))
           .ToList();
    }
    // ------------------------------------------------------------------------------------------------




    // This private method is responsible for mapping a Trainer entity to a TrainerResponse DTO.
    private static TrainerResponse MapToResponse(Trainer trainer)
        => new(
            Id: trainer.Id,
            FullName: trainer.FullName,
            Specialty: trainer.Specialty,
            CreatedAt: trainer.CreatedAt,
            UpdatedAt: trainer.UpdatedAt
        );
 
}
