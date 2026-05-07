namespace Gym.Infrastructure.Data.Configurations;

public class MembershipPlanConfiguration : IEntityTypeConfiguration<MembershipPlan>
{
    public void Configure(EntityTypeBuilder<MembershipPlan> builder)
    {
        // Table Name
        builder.ToTable("MembershipPlans");

        // Primary Key
        builder.HasKey(x => x.Id);

        // Properties

        builder.Property(x => x.Name)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(x => x.Type)
               .IsRequired();

        builder.Property(x => x.Price)
               .HasColumnType("decimal(10,2)");

        builder.Property(x => x.Description)
               .IsRequired()
               .HasMaxLength(500);

        builder.Property(x => x.MaxSessionsPerMonth)
               .IsRequired();

        builder.Property(x => x.IncludesPersonalTrainer)
               .IsRequired();

        builder.Property(x => x.IsActive)
               .IsRequired();

        // Relationships

        // MembershipPlan -> Members
        builder.HasMany(x => x.Members)
               .WithOne(x => x.MembershipPlan)
               .HasForeignKey(x => x.MembershipPlanId);
    }
}