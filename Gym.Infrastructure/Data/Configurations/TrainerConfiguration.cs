
namespace Gym.Infrastructure.Data.Configurations;

public class TrainerConfiguration : IEntityTypeConfiguration<Trainer>
{
    public void Configure(EntityTypeBuilder<Trainer> builder)
    {
        // Table Name In DataBase
        builder.ToTable("Trainers");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.FullName).IsRequired().HasMaxLength(150);
        builder.Property(x => x.Specialty).IsRequired().HasMaxLength(100);
        builder.HasMany(x => x.Sessions).WithOne(x => x.Trainer).HasForeignKey(x => x.TrainerId);
    }
}
