
namespace Gym.Domain.Entities;

public class Trainer : BaseEntity
{
    public string FullName { get; private set; } = null!;
    public string Specialty { get; private set; } = null!;

    private readonly List<Session> _sessions = new();
    public IReadOnlyCollection<Session> Sessions => _sessions;

    private Trainer() { }

    public Trainer(string fullName, string specialty)
    => (FullName, Specialty) = (fullName, specialty);
}

// You Should Know Why You Make The Set Accessor Is Private 
// the new Syntax for initializing new list like list<Session>
// What Is the Defference Between IReadOnlyCollection and IEnumerable
// Using expression-bodied members for constructor
