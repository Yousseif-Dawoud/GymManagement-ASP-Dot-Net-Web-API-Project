namespace Gym.Domain.Entities;

public class Package : BaseEntity
{
    // Core Info
    // =========================
    public string Name { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public bool IsActive { get; private set; }

    // Optional Marketing Fields (Future / Campaigns)
    // =========================
    public decimal? DiscountPercentage { get; private set; }
    public int? BonusSessions { get; private set; }
    public DateOnly? StartDate { get; private set; }
    public DateOnly? EndDate { get; private set; }


    // Relationships
    // =========================
    public int MembershipPlanId { get; private set; }
    public MembershipPlan MembershipPlan { get; private set; } = null!;

    private readonly List<Member> _members = new();
    public IReadOnlyCollection<Member> Members => _members;


    // EF Core
    // =========================
    private Package() { }


    // Constructor
    // =========================
    public Package(
        string name,
        string description,
        int membershipPlanId,
        decimal? discountPercentage = null,
        int? bonusSessions = null,
        DateOnly? startDate = null,
        DateOnly? endDate = null)
    {
        Validate(name, description);

        Name = name.Trim();
        Description = description.Trim();
        MembershipPlanId = membershipPlanId;

        DiscountPercentage = discountPercentage;
        BonusSessions = bonusSessions;
        StartDate = startDate;
        EndDate = endDate;

        IsActive = true;
    }


    // Business Methods
    // =========================
    public void Update(
        string name,
        string description,
        int membershipPlanId,
        decimal? discountPercentage,
        int? bonusSessions,
        DateOnly? startDate,
        DateOnly? endDate)
    {
        Validate(name, description);

        Name = name.Trim();
        Description = description.Trim();
        MembershipPlanId = membershipPlanId;

        DiscountPercentage = discountPercentage;
        BonusSessions = bonusSessions;
        StartDate = startDate;
        EndDate = endDate;

        SetUpdated();
    }


    public void Activate()
    {
        if (IsActive) return;

        IsActive = true;
        SetUpdated();
    }

    public void Deactivate()
    {
        if (!IsActive) return;

        IsActive = false;
        SetUpdated();
    }


    // Validation
    // =========================
    private static void Validate(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new BusinessRuleException("Package name is required.");

        if (string.IsNullOrWhiteSpace(description))
            throw new BusinessRuleException("Package description is required.");
    }
}