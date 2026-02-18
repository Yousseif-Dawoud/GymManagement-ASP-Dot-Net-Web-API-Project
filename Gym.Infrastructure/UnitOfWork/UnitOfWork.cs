


using Gym.Domain.Common;

namespace Gym.Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    // Inject the DbContext and initialize repositories
    private readonly GymDbContext _context;
    private readonly ConcurrentDictionary<Type, object> _repositories = new();

    // Expose repositories for each entity type
    public IGenericRepository<Member> Members { get;}
    public IGenericRepository<Trainer> Trainers { get; }
    public IGenericRepository<Session> Sessions { get; }
    public IGenericRepository<Booking> Bookings { get; }
    public IGenericRepository<MembershipPlan> MembershipPlans { get; }



    // Constructor to initialize the UnitOfWork with the DbContext and repositories
    public UnitOfWork(GymDbContext context)
    {
        _context = context;
        Members = new GenericRepository<Member>(_context);
        Trainers = new GenericRepository<Trainer>(_context);
        Sessions = new GenericRepository<Session>(_context);
        Bookings = new GenericRepository<Booking>(_context);
        MembershipPlans = new GenericRepository<MembershipPlan>(_context);
    }

    public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
    {
        // Return the appropriate repository based on the entity type
        var type = typeof(TEntity);

        // Check if the requested type matches any of the known entity types and return it
        if (type == typeof(Member)) return (IGenericRepository<TEntity>)Members;
        if (type == typeof(Trainer)) return (IGenericRepository<TEntity>)Trainers;
        if (type == typeof(Session)) return (IGenericRepository<TEntity>)Sessions;
        if (type == typeof(Booking)) return (IGenericRepository<TEntity>)Bookings;
        if (type == typeof(MembershipPlan)) return (IGenericRepository<TEntity>)MembershipPlans;

        // If the type is not one of the known entity types, create a new repository 
        return (IGenericRepository<TEntity>)_repositories.GetOrAdd(type, _ => new GenericRepository<TEntity>(_context));
    }



    // Save changes to the database
    public Task<int> SaveChangesAsync(CancellationToken ct = default)
        => _context.SaveChangesAsync(ct);



    // Dispose the DbContext when done
    public void Dispose() => _context.Dispose();

}
