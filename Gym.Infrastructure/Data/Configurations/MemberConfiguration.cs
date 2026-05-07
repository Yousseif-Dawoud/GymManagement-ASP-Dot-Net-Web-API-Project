namespace Gym.Infrastructure.Data.Configurations;

public class MemberConfiguration : IEntityTypeConfiguration<Member>
{
    public void Configure(EntityTypeBuilder<Member> builder)
    {
        // Table Name
        builder.ToTable("Members");

        // Primary Key
        builder.HasKey(x => x.Id);

        // Properties
        builder.Property(x => x.FullName)
               .IsRequired()
               .HasMaxLength(150);

        builder.Property(x => x.Phone)
               .IsRequired()
               .HasMaxLength(20);

        builder.Property(x => x.Email)
               .IsRequired()
               .HasMaxLength(150);

        builder.Property(x => x.MembershipStartDate)
               .HasColumnType("date");

        builder.Property(x => x.MembershipEndDate)
               .HasColumnType("date");

        builder.Property(x => x.Status)
               .IsRequired();

        // Relationships

        // Member -> MembershipPlan (Many Members Have One Plan)
        builder.HasOne(x => x.MembershipPlan)
               .WithMany(x => x.Members)
               .HasForeignKey(x => x.MembershipPlanId)
               .OnDelete(DeleteBehavior.Restrict);

        // Member -> Bookings (One Member Has Many Bookings)
        builder.HasMany(x => x.Bookings)
               .WithOne(x => x.Member)
               .HasForeignKey(x => x.MemberId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}