namespace Gym.Infrastructure.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class 
{
    // Inject the DbContext To Do Database Operations and Set the DbSet for the specific entity type
    protected readonly GymDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;
    public GenericRepository(GymDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = _context.Set<TEntity>();
    }


    // Get all entities from the database 
    public  async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken ct = default)
    => await _dbSet.AsNoTracking().ToListAsync(ct);



    // Get an entity by its primary key (id) using FindAsync
    public Task<TEntity?> GetByIdAsync(int id ,CancellationToken ct = default)
    => _dbSet.FindAsync(id, ct).AsTask();



    // Find entities that match a given predicate (filter)
    public  async Task<IReadOnlyList<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct = default)
    => await _dbSet.Where(predicate).AsNoTracking().ToListAsync(ct);



    // Add a new entity to the database using AddAsync
    public  Task AddAsync(TEntity entity , CancellationToken ct = default)
    => _dbSet.AddAsync(entity, ct).AsTask();



    // Update the entity by attaching it to the context and setting its state to Modified
    public void Update(TEntity entity)
    {
        _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }



    // Remove the entity from the database using Remove
    public void Remove(TEntity entity) => _dbSet.Remove(entity);


}
