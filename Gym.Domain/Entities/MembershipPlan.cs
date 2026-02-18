
namespace Gym.Domain.Entities;

public class MembershipPlan : BaseEntity
{
    public string Name { get; protected set; } = null!;
    public decimal Price { get; protected set; }
    public int MaxSessionsPerMonth { get; set; }
    private MembershipPlan() { } // For EF Core


    public MembershipPlan(string name, decimal price, int maxSessionsPerMonth)
    => (Name, Price, MaxSessionsPerMonth) = (name, price, maxSessionsPerMonth);
}
