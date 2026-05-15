namespace Gym.Infrastructure.Data.Configurations;

public class MemberConfiguration : IEntityTypeConfiguration<Member>
{
    public void Configure(EntityTypeBuilder<Member> builder)
    {
        // Table
        // =========================

        builder.ToTable("Members");


        // Primary Key
        // =========================

        builder.HasKey(x => x.Id);


        // Personal Information
        // =========================

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
               .HasColumnType("date");

        builder.Property(x => x.EmergencyContact)
               .HasMaxLength(20);


        // Membership Information
        // =========================

        builder.Property(x => x.MembershipStartDate)
               .HasColumnType("date");

        builder.Property(x => x.MembershipEndDate)
               .HasColumnType("date");

        builder.Property(x => x.Status)
               .IsRequired();


        // Relationships
        // =========================

        // Member -> MembershipPlan
        // Many Members belong to one MembershipPlan

        builder.HasOne(x => x.MembershipPlan)
               .WithMany(x => x.Members)
               .HasForeignKey(x => x.MembershipPlanId)
               .OnDelete(DeleteBehavior.Restrict);


        // Member -> Package
        // Many Members may belong to one Package

        builder.HasOne(x => x.Package)
               .WithMany(x => x.Members)
               .HasForeignKey(x => x.PackageId)
               .OnDelete(DeleteBehavior.SetNull)
               .IsRequired(false);


        // Member -> Bookings
        // One Member has many Bookings

        builder.HasMany(x => x.Bookings)
               .WithOne(x => x.Member)
               .HasForeignKey(x => x.MemberId)
               .OnDelete(DeleteBehavior.Restrict);


        // Indexes
        // =========================

        builder.HasIndex(x => x.Email)
               .IsUnique();

        builder.HasIndex(x => x.Phone)
               .IsUnique();
    }
}