namespace Gym.Infrastructure.Data.Configurations;

public class TrainerConfiguration : IEntityTypeConfiguration<Trainer>
{
    public void Configure(EntityTypeBuilder<Trainer> builder)
    {
        // Table
        // ======================
        builder.ToTable("Trainers");

        // Primary Key
        // ======================
        builder.HasKey(x => x.Id);

        // Properties
        // ======================

        builder.Property(x => x.FullName)
               .IsRequired()
               .HasMaxLength(150);

        builder.Property(x => x.Phone)
               .IsRequired()
               .HasMaxLength(20);

        builder.Property(x => x.Email)
               .IsRequired()
               .HasMaxLength(150);

        builder.Property(x => x.Gender)
               .IsRequired();

        builder.Property(x => x.DateOfBirth)
               .HasColumnType("date")
               .IsRequired();

        builder.Property(x => x.Specialization)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(x => x.ExperienceYears)
               .IsRequired();

        builder.Property(x => x.HireDate)
               .HasColumnType("date")
               .IsRequired();

        builder.Property(x => x.Bio)
               .HasMaxLength(500);

        builder.Property(x => x.Status)
               .IsRequired();

        // Indexes
        // ======================
        builder.HasIndex(x => x.Email)
               .IsUnique();

        // Relationships
        // ======================

        builder.HasMany(x => x.Sessions)
               .WithOne(x => x.Trainer)
               .HasForeignKey(x => x.TrainerId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}