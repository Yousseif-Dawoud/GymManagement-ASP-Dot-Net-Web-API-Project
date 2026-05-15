namespace Gym.Application.Interfaces.IUnitOfWork;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<Member> Members { get; }
    IGenericRepository<Trainer> Trainers { get; }
    IGenericRepository<Session> Sessions { get; }
    IGenericRepository<Booking> Bookings { get; }
    IGenericRepository<MembershipPlan> MembershipPlans { get; }
    IGenericRepository<Package> Packages { get; }

    // Generic repository for any entity type 
    IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;

    // Method to save changes to the database
    Task<int> SaveChangesAsync(CancellationToken ct = default);
}
