

namespace Gym.Domain.Entities;

public class Member : BaseEntity
{
    // Personal Information
    // =========================
    public string FullName { get; private set; } = null!;
    public string Phone { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public Gender Gender { get; private set; }
    public DateOnly DateOfBirth { get; private set; }
    public string? EmergencyContact { get; private set; }


    // Membership Information
    // =========================
    public DateOnly MembershipStartDate { get; private set; }
    public DateOnly MembershipEndDate { get; private set; }
    public MembershipStatus Status { get; private set; }


    // Relationships
    // =========================

    // Required Membership Plan
    public int MembershipPlanId { get; private set; }
    public MembershipPlan MembershipPlan { get; private set; } = null!;

    // Optional Package
    public int? PackageId { get; private set; }
    public Package? Package { get; private set; }

    // Bookings
    private readonly List<Booking> _bookings = new();
    public IReadOnlyCollection<Booking> Bookings => _bookings;


    // EF Core Constructor
    // =========================
    private Member() { }


    // Main Constructor
    // =========================
    public Member(
        string fullName,
        string phone,
        string email,
        Gender gender,
        DateOnly dateOfBirth,
        string? emergencyContact,
        DateOnly membershipStartDate,
        DateOnly membershipEndDate,
        int membershipPlanId,
        int? packageId = null)
    {
        Validate(fullName,phone,email,membershipStartDate,membershipEndDate);

        FullName = fullName.Trim();
        Phone = phone.Trim();
        Email = email.Trim().ToLowerInvariant();
        Gender = gender;
        DateOfBirth = dateOfBirth;
        EmergencyContact = emergencyContact?.Trim();
        MembershipStartDate = membershipStartDate;
        MembershipEndDate = membershipEndDate;
        MembershipPlanId = membershipPlanId;
        PackageId = packageId;
        Status = MembershipStatus.Active;
    }

    // Update Member
    // =========================

    public void Update(
        string fullName,
        string phone,
        string email,
        Gender gender,
        DateOnly dateOfBirth,
        string? emergencyContact,
        DateOnly membershipStartDate,
        DateOnly membershipEndDate,
        int membershipPlanId,
        int? packageId = null)
    {
        Validate(fullName, phone, email, membershipStartDate, membershipEndDate);

        FullName = fullName.Trim();
        Phone = phone.Trim();
        Email = email.Trim().ToLowerInvariant();
        Gender = gender;
        DateOfBirth = dateOfBirth;
        EmergencyContact = emergencyContact?.Trim();
        MembershipStartDate = membershipStartDate;
        MembershipEndDate = membershipEndDate;
        MembershipPlanId = membershipPlanId;
        PackageId = packageId;
        SetUpdated();
    }

    // Membership Behaviors
    // =========================

    public void RenewMembership(DateOnly newStartDate,DateOnly newEndDate)
    {
        ValidateMembershipDates(newStartDate,newEndDate);
        MembershipStartDate = newStartDate;
        MembershipEndDate = newEndDate;
        Status = MembershipStatus.Active;
        SetUpdated();
    }

    public void ExpireMembership()
    {
        if (Status == MembershipStatus.Expired)
            return;

        Status = MembershipStatus.Expired;

        SetUpdated();
    }

    public void FreezeMembership()
    {
        if (Status == MembershipStatus.Frozen)
            return;

        Status = MembershipStatus.Frozen;

        SetUpdated();
    }

    public void UnfreezeMembership()
    {
        if (Status != MembershipStatus.Frozen)
            return;

        Status = MembershipStatus.Active;

        SetUpdated();
    }

    // Package Behaviors
    // =========================
    public void AssignPackage(int packageId)
    {
        PackageId = packageId;

        SetUpdated();
    }

    public void RemovePackage()
    {
        PackageId = null;

        SetUpdated();
    }

    // Validation
    // =========================

    private static void Validate(
        string fullName,
        string phone,
        string email,
        DateOnly membershipStartDate,
        DateOnly membershipEndDate)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            throw new BusinessRuleException(
                "Full name is required.");

        if (string.IsNullOrWhiteSpace(phone))
            throw new BusinessRuleException(
                "Phone number is required.");

        if (string.IsNullOrWhiteSpace(email))
            throw new BusinessRuleException(
                "Email is required.");

        ValidateMembershipDates(membershipStartDate,membershipEndDate);
    }

    private static void ValidateMembershipDates(DateOnly startDate,DateOnly endDate)
    {
        if (endDate < startDate)
            throw new BusinessRuleException(
                "Membership end date cannot be earlier than start date.");
    }


}

