
namespace Gym.Domain.Entities;

public class MembershipPlan : BaseEntity
{
    // Properties
    public string Name { get; private set; } = null!;

    public MembershipPlanType Type { get; private set; }

    public decimal Price { get; private set; }

    public string Description { get; private set; } = null!;

    public int MaxSessionsPerMonth { get; private set; }

    public bool IncludesPersonalTrainer { get; private set; }

    public bool IsActive { get; private set; }

    // Navigation Properties
    private readonly List<Member> _members = new();
    public IReadOnlyCollection<Member> Members => _members;

    // Private Constructor For EF Core
    private MembershipPlan() { }

    // Public Constructor To Create A New Membership Plan
    public MembershipPlan(
        string name,
        MembershipPlanType type,
        decimal price,
        string description,
        int maxSessionsPerMonth,
        bool includesPersonalTrainer)
    {
        Validate(name, price, maxSessionsPerMonth);

        Name = name;
        Type = type;
        Price = price;
        Description = description;
        MaxSessionsPerMonth = maxSessionsPerMonth;
        IncludesPersonalTrainer = includesPersonalTrainer;

        IsActive = true;
    }


    // Business Logic Methods

    public void Update(
        string name,
        MembershipPlanType type,
        decimal price,
        string description,
        int maxSessionsPerMonth,
        bool includesPersonalTrainer)
    {
        Validate(name, price, maxSessionsPerMonth);

        Name = name;
        Type = type;
        Price = price;
        Description = description;
        MaxSessionsPerMonth = maxSessionsPerMonth;
        IncludesPersonalTrainer = includesPersonalTrainer;

        SetUpdated();
    }

    public void Activate() 
    {
        IsActive = true;
        SetUpdated();
    }

    public void Deactivate()  
    {
        IsActive = false;
        SetUpdated();
    }

    private static void Validate(
       string name,
       decimal price,
       int maxSessionsPerMonth)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new BusinessRuleException("Plan name is required.");

        if (price <= 0)
            throw new BusinessRuleException(
                "Price must be greater than zero.");

        if (maxSessionsPerMonth <= 0)
            throw new BusinessRuleException(
                "Max sessions per month must be greater than zero.");
    }
}
