
namespace Gym.Application.Interfaces.IRepositories;

public interface IGenericRepository<TEntity> where TEntity : class
{
    // Get all entities
    Task <IReadOnlyList<TEntity>> GetAllAsync(CancellationToken ct = default);


    // Get an entity by its ID
    Task<TEntity?> GetByIdAsync(int id , CancellationToken ct = default);


    // Find entities based on a predicate
    Task<IReadOnlyList<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct= default);


    // Add a new entity
    Task AddAsync(TEntity entity , CancellationToken ct = default);


    // Update an existing entity
    void Update(TEntity entity);


    // Delete an entity
    void Remove(TEntity entity);
}
