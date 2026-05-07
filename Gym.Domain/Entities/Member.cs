

namespace Gym.Domain.Entities;

public class Member : BaseEntity
{
    public string FullName { get; private set; } = null!;
    public string Phone { get; private set; } = null!;
    public string Email { get; private set; } = null!;

    public DateOnly MembershipStartDate { get; private set; }
    public DateOnly MembershipEndDate { get; private set; }
    public MembershipStatus Status { get; private set; }
    public int MembershipPlanId { get; private set; }
    public MembershipPlan MembershipPlan { get; private set; } = null!; // Navigation property

    private readonly List<Booking> _bookings = new();
    public IReadOnlyCollection<Booking> Bookings => _bookings;

    public Member(
    string fullName,
    string phone,
    string email,
    DateOnly membershipStartDate,
    DateOnly membershipEndDate,
    int membershipPlanId)
    {
        ValidateMembershipDates( membershipStartDate, membershipEndDate);

        FullName = fullName;
        Phone = phone;
        Email = email;
        MembershipStartDate = membershipStartDate;
        MembershipEndDate = membershipEndDate;
        MembershipPlanId = membershipPlanId;
        Status = MembershipStatus.Active;
    }

    public void Update(
    string fullName,
    string phone,
    string email,
    DateOnly membershipStartDate,
    DateOnly membershipEndDate,
    int membershipPlanId)
    {
        ValidateMembershipDates(membershipStartDate, membershipEndDate);

        FullName = fullName;
        Phone = phone;
        Email = email.Trim().ToLowerInvariant();
        MembershipStartDate = membershipStartDate;
        MembershipEndDate = membershipEndDate;
        MembershipPlanId = membershipPlanId;

        SetUpdated();
    }

    public void ExpireMembership()
    {
            Status = MembershipStatus.Expired;
            SetUpdated();
    }

    // Private Validation Method
    // =========================
    private static void ValidateMembershipDates(
        DateOnly startDate,
        DateOnly endDate)
    {
        if (endDate < startDate)
            throw new BusinessRuleException(
                "Membership end date cannot be earlier than start date.");
    }
}

