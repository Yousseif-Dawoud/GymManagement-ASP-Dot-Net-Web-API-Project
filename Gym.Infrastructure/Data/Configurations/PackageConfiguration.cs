namespace Gym.Infrastructure.Data.Configurations;

public class PackageConfiguration : IEntityTypeConfiguration<Package>
{
    public void Configure(EntityTypeBuilder<Package> builder)
    {
        // Table
        // ======================
        builder.ToTable("Packages");

        // Primary Key
        // ======================
        builder.HasKey(x => x.Id);

        // Properties
        // ======================

        builder.Property(x => x.Name)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(x => x.Description)
               .IsRequired()
               .HasMaxLength(500);

        builder.Property(x => x.IsActive)
               .IsRequired();


        // Optional Marketing Fields
        builder.Property(x => x.DiscountPercentage)
               .HasColumnType("decimal(5,2)");

        builder.Property(x => x.BonusSessions);

        builder.Property(x => x.StartDate)
               .HasColumnType("date");

        builder.Property(x => x.EndDate)
               .HasColumnType("date");


        // Relationships
        // ======================

        // Package -> MembershipPlan (Many Packages per Plan)
        builder.HasOne(x => x.MembershipPlan)
               .WithMany(x => x.Packages)
               .HasForeignKey(x => x.MembershipPlanId)
               .OnDelete(DeleteBehavior.Restrict);


        // Package -> Members (One Package has many Members)
        builder.HasMany(x => x.Members)
               .WithOne(x => x.Package)
               .HasForeignKey(x => x.PackageId)
               .OnDelete(DeleteBehavior.SetNull);


        // Indexes (Performance)
        // ======================

        builder.HasIndex(x => x.MembershipPlanId);

        builder.HasIndex(x => x.IsActive);

        builder.HasIndex(x => new { x.StartDate, x.EndDate });
    }
}