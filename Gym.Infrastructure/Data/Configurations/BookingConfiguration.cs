namespace Gym.Infrastructure.Data.Configurations;

public class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        // Table
        // =========================
        builder.ToTable("Bookings");


        // Primary Key
        // =========================
        builder.HasKey(x => x.Id);


        // Properties
        // =========================
        builder.Property(x => x.BookingDate)
               .IsRequired()
               .HasColumnType("date");

        builder.Property(x => x.Status)
               .IsRequired();

        builder.Property(x => x.Notes)
               .HasMaxLength(500);


        // Relationships
        // =========================

        // Booking -> Member
        // Many Bookings belong to one Member

        builder.HasOne(x => x.Member)
               .WithMany(x => x.Bookings)
               .HasForeignKey(x => x.MemberId)
               .OnDelete(DeleteBehavior.Restrict);


        // Booking -> Session
        // Many Bookings belong to one Session

        builder.HasOne(x => x.Session)
               .WithMany(x => x.Bookings)
               .HasForeignKey(x => x.SessionId)
               .OnDelete(DeleteBehavior.Restrict);


        // Indexes
        // =========================

        // Prevent duplicate booking
        // One member cannot book the same session twice

        builder.HasIndex(x => new { x.MemberId, x.SessionId })
               .IsUnique();


        // Additional Constraints
        // =========================

        builder.Property(x => x.CreatedAt)
               .IsRequired();

        builder.Property(x => x.UpdatedAt);
    }
}