
namespace Gym.Domain.Common;

public abstract class BaseEntity
{
    public int Id { get; protected set; }
    public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; protected set; }

    protected void SetUpdated() => UpdatedAt = DateTime.UtcNow;
}

// Why The Class Is Abstract ? 
// What Is the Meanig The Set Accessor Is Protected ?
// Why I`m Choise DateTime ?
// What Defference Between DateTime.Now And DateTime.UtcNow ?

