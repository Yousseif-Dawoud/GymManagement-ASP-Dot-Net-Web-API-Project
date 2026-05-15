namespace Gym.Domain.Entities;

public class MembershipPlan : BaseEntity
{
    // Properties
    // =========================
    public MembershipPlanType Type { get; private set; }
    public decimal Price { get; private set; }
    public string Description { get; private set; } = null!;
    public int DurationInDays { get; private set; }
    public int MaxSessionsPerMonth { get; private set; }
    public bool IncludesPersonalTrainer { get; private set; }
    public bool IsActive { get; private set; }


    // Navigation Properties
    // =========================

    private readonly List<Member> _members = new();
    public IReadOnlyCollection<Member> Members => _members;

    private readonly List<Package> _packages = new();
    public IReadOnlyCollection<Package> Packages => _packages;


    // EF Core Constructor
    // =========================
    private MembershipPlan() { }


    // Main Constructor
    // =========================
    public MembershipPlan(
        MembershipPlanType type,
        decimal price,
        string description,
        int durationInDays,
        int maxSessionsPerMonth,
        bool includesPersonalTrainer = false)
    {
        Validate(price,description, durationInDays,maxSessionsPerMonth);

        Type = type;
        Price = price;
        Description = description.Trim();
        DurationInDays = durationInDays;
        MaxSessionsPerMonth = maxSessionsPerMonth;

        ApplyPersonalTrainerRule(type,includesPersonalTrainer);

        IsActive = true;
    }


    // Update
    // =========================

    public void Update(
        MembershipPlanType type,
        decimal price,
        string description,
        int durationInDays,
        int maxSessionsPerMonth,
        bool includesPersonalTrainer)
    {
        Validate(price, description, durationInDays, maxSessionsPerMonth);

        Type = type;
        Price = price;
        Description = description.Trim();
        DurationInDays = durationInDays;
        MaxSessionsPerMonth = maxSessionsPerMonth;

        ApplyPersonalTrainerRule(type, includesPersonalTrainer);

        SetUpdated();
    }


    // Business Behaviors
    // =========================

    public void Activate()
    {
        if (IsActive)
            return;

        IsActive = true;

        SetUpdated();
    }

    public void Deactivate()
    {
        if (!IsActive)
            return;

        IsActive = false;

        SetUpdated();
    }

    public void ChangePrice(decimal newPrice)
    {
        if (newPrice <= 0)
            throw new BusinessRuleException("Price must be greater than zero.");

        Price = newPrice;

        SetUpdated();
    }

    public void ChangeDuration(int durationInDays)
    {
        if (durationInDays <= 0)
            throw new BusinessRuleException("Duration must be greater than zero.");

        DurationInDays = durationInDays;

        SetUpdated();
    }

    public void ChangeSessionLimit(int maxSessionsPerMonth)
    {
        if (maxSessionsPerMonth <= 0)
            throw new BusinessRuleException("Max sessions per month must be greater than zero.");

        MaxSessionsPerMonth = maxSessionsPerMonth;

        SetUpdated();
    }

    public void ChangeDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new BusinessRuleException("Description is required.");

        Description = description.Trim();

        SetUpdated();
    }


    // Business Rules
    // =========================

    private void ApplyPersonalTrainerRule(MembershipPlanType type,bool includesPersonalTrainer)
    {
        // VIP plans must always include PT

        IncludesPersonalTrainer = type == MembershipPlanType.VIP ? true : includesPersonalTrainer;
    }


    // Validation
    // =========================

    private static void Validate(
        decimal price,
        string description,
        int durationInDays,
        int maxSessionsPerMonth)
    {
        if (price <= 0)
            throw new BusinessRuleException("Price must be greater than zero.");

        if (string.IsNullOrWhiteSpace(description))
            throw new BusinessRuleException("Description is required.");

        if (durationInDays <= 0)
            throw new BusinessRuleException("Duration must be greater than zero.");

        if (maxSessionsPerMonth <= 0)
            throw new BusinessRuleException("Max sessions per month must be greater than zero.");
    }
}