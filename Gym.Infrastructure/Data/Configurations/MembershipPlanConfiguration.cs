
namespace Gym.Infrastructure.Data.Configurations;

public class MembershipPlanConfiguration : IEntityTypeConfiguration<MembershipPlan>
{
    public void Configure(EntityTypeBuilder<MembershipPlan> builder)
    {
        // Table Name In DataBase
        builder.ToTable("MembershipPlans");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Price).HasColumnType("decimal(10,2)");
        builder.Property(x => x.MaxSessionsPerMonth).IsRequired();
    }
}
