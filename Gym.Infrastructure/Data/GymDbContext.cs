public class GymDbContext : DbContext
{
    public GymDbContext(DbContextOptions<GymDbContext> options) : base(options) { }

    public DbSet<Member> Members => Set<Member>();
    public DbSet<Trainer> Trainers => Set<Trainer>();
    public DbSet<Session> Sessions => Set<Session>();
    public DbSet<Booking> Bookings => Set<Booking>();
    public DbSet<MembershipPlan> MembershipPlans => Set<MembershipPlan>();

    // ✅ NEW
    public DbSet<Package> Packages => Set<Package>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(GymDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}