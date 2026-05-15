namespace Gym.Infrastructure.Data.Configurations;

public class SessionConfiguration : IEntityTypeConfiguration<Session>
{
    public void Configure(EntityTypeBuilder<Session> builder)
    {
        // Table
        // =========================
        builder.ToTable("Sessions");


        // Primary Key
        // =========================
        builder.HasKey(x => x.Id);


        // Properties
        // =========================

        builder.Property(x => x.Name)
               .IsRequired()
               .HasMaxLength(150);

        builder.Property(x => x.Description)
               .HasMaxLength(1000);

        builder.Property(x => x.Type)
               .IsRequired();

        builder.Property(x => x.Status)
               .IsRequired();

        builder.Property(x => x.StartTime)
               .IsRequired();

        builder.Property(x => x.EndTime)
               .IsRequired();

        builder.Property(x => x.Capacity)
               .IsRequired();


        // Relationships
        // =========================

        // Session -> Trainer
        // Many Sessions belong to one Trainer
        builder.HasOne(x => x.Trainer)
               .WithMany(x => x.Sessions)
               .HasForeignKey(x => x.TrainerId)
               .OnDelete(DeleteBehavior.Restrict);


        // Session -> Bookings
        // One Session has many Bookings
        builder.HasMany(x => x.Bookings)
               .WithOne(x => x.Session)
               .HasForeignKey(x => x.SessionId)
               .OnDelete(DeleteBehavior.Restrict);


        // Indexes
        // =========================
        builder.HasIndex(x => x.StartTime);
        builder.HasIndex(x => x.TrainerId);
        builder.HasIndex(x => x.Status);
    }
}