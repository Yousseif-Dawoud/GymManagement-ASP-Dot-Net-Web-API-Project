using Gym.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gym.Infrastructure.Data.Configurations;

public class MembershipPlanConfiguration
    : IEntityTypeConfiguration<MembershipPlan>
{
    public void Configure(EntityTypeBuilder<MembershipPlan> builder)
    {
        // Table
        // =========================

        builder.ToTable("MembershipPlans");


        // Primary Key
        // =========================

        builder.HasKey(x => x.Id);


        // Properties
        // =========================

        builder.Property(x => x.Type)
               .IsRequired();

        builder.Property(x => x.Price)
               .IsRequired()
               .HasColumnType("decimal(10,2)");

        builder.Property(x => x.Description)
               .IsRequired()
               .HasMaxLength(500);

        builder.Property(x => x.DurationInDays)
               .IsRequired();

        builder.Property(x => x.MaxSessionsPerMonth)
               .IsRequired();

        builder.Property(x => x.IncludesPersonalTrainer)
               .IsRequired();

        builder.Property(x => x.IsActive)
               .IsRequired();


        // Indexes
        // =========================

        // Prevent duplicate plan types
        // Example:
        // Only one VIP
        // Only one Premium
        // Only one Basic

        builder.HasIndex(x => x.Type)
               .IsUnique();


        // Relationships
        // =========================

        // MembershipPlan -> Members
        // One Plan has many Members

        builder.HasMany(x => x.Members)
               .WithOne(x => x.MembershipPlan)
               .HasForeignKey(x => x.MembershipPlanId)
               .OnDelete(DeleteBehavior.Restrict);


        // MembershipPlan -> Packages
        // One Plan has many Packages

        builder.HasMany(x => x.Packages)
               .WithOne(x => x.MembershipPlan)
               .HasForeignKey(x => x.MembershipPlanId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}