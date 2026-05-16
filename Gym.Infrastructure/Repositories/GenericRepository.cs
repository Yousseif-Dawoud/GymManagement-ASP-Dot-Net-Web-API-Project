namespace Gym.Infrastructure.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
    // Injecting the DbContext and initializing the DbSet for the entity type
    private readonly GymDbContext _context;
    private readonly DbSet<TEntity> _dbSet;
    public GenericRepository(GymDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = _context.Set<TEntity>();
    }

    // READ OPERATIONS
    // =========================
    public async Task<TEntity?> GetByIdAsync(int id,CancellationToken ct = default)
            => await _dbSet.FindAsync(new object[] { id }, ct);
    

    public async Task<IReadOnlyList<TEntity>> FindAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken ct = default)
    {
        return await _dbSet
            .Where(predicate)
            .AsNoTracking()
            .ToListAsync(ct);
    }

    public async Task<bool> ExistsAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken ct = default)
    {
        return await _dbSet.AnyAsync(predicate, ct);
    }

    // ❌ Removed GetAllAsync (to avoid unsafe full table loading)

    // WRITE OPERATIONS
    // =========================
    public async Task AddAsync(
        TEntity entity,
        CancellationToken ct = default)
    {
        if (entity is null)
            throw new ArgumentNullException(nameof(entity));

        await _dbSet.AddAsync(entity, ct);
    }

    public void Update(TEntity entity)
    {
        if (entity is null)
            throw new ArgumentNullException(nameof(entity));

        _dbSet.Update(entity);
    }

    public void Remove(TEntity entity)
    {
        if (entity is null)
            throw new ArgumentNullException(nameof(entity));

        _dbSet.Remove(entity);
    }
}