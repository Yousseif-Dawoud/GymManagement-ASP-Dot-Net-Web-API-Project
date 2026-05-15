namespace Gym.Domain.Entities;

public class Session : BaseEntity
{
    // Basic Information
    // =========================
    public string Name { get; private set; } = null!;
    public string? Description { get; private set; }


    // Session Details
    // =========================
    public SessionType Type { get; private set; }
    public SessionStatus Status { get; private set; }
    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }
    public int Capacity { get; private set; }


    // Relationships
    // =========================

    // Trainer
    public int TrainerId { get; private set; }
    public Trainer Trainer { get; private set; } = null!;

    // Bookings
    private readonly List<Booking> _bookings = new();
    public IReadOnlyCollection<Booking> Bookings => _bookings;


    // EF Core Constructor
    // =========================
    private Session() { }


    // Main Constructor
    // =========================

    public Session(
        string name,
        string? description,
        SessionType type,
        DateTime startTime,
        DateTime endTime,
        int capacity,
        int trainerId)
    {
        Validate(name,startTime,endTime,capacity);

        ApplySessionTypeRule(type, capacity);

        Name = name.Trim();
        Description = description?.Trim();
        Type = type;
        StartTime = startTime;
        EndTime = endTime;
        Capacity = capacity;
        TrainerId = trainerId;
        Status = SessionStatus.Scheduled;
    }


    // Update Session
    // =========================
    public void Update(
        string name,
        string? description,
        SessionType type,
        DateTime startTime,
        DateTime endTime,
        int capacity,
        int trainerId)
    {
        EnsureSessionIsEditable();

        Validate(name,startTime,endTime,capacity);

        ApplySessionTypeRule(type, capacity);

        Name = name.Trim();
        Description = description?.Trim();
        Type = type;
        StartTime = startTime;
        EndTime = endTime;
        Capacity = capacity;
        TrainerId = trainerId;
        SetUpdated();
    }


    // Session Behaviors
    // =========================

    public void Cancel()
    {
        if (Status == SessionStatus.Cancelled)
            return;

        if (Status == SessionStatus.Completed)
            throw new BusinessRuleException("Cannot cancel a completed session.");

        Status = SessionStatus.Cancelled;

        SetUpdated();
    }

    public void Complete()
    {
        if (Status == SessionStatus.Completed)
            return;

        if (Status == SessionStatus.Cancelled)
            throw new BusinessRuleException("Cannot complete a cancelled session.");

        Status = SessionStatus.Completed;

        SetUpdated();
    }

    public void Reschedule(DateTime newStartTime,DateTime newEndTime)
    {
        EnsureSessionIsEditable();

        ValidateSessionTime(newStartTime,newEndTime);

        StartTime = newStartTime;
        EndTime = newEndTime;

        SetUpdated();
    }

    public void ChangeCapacity(int newCapacity)
    {
        EnsureSessionIsEditable();

        if (newCapacity <= 0)
            throw new BusinessRuleException("Capacity must be greater than zero.");

        if (Type == SessionType.Personal && newCapacity != 1)
                throw new BusinessRuleException("Personal session capacity must be exactly 1.");

        Capacity = newCapacity;

        SetUpdated();
    }


    // Business Rules
    // =========================
    private static void ApplySessionTypeRule(SessionType type,int capacity)
    {
        if (type == SessionType.Personal && capacity != 1)
            throw new BusinessRuleException("Personal session capacity must be exactly 1.");
    }


    // Validation
    // =========================
    private static void Validate(string name,DateTime startTime,DateTime endTime,int capacity)
    {
        // Check On the Session Name Is Not Empty or Whitespace and Throw an Exception if It Is
        if (string.IsNullOrWhiteSpace(name))
            throw new BusinessRuleException("Session name is required.");
        
        // Make Sure The Time of the Session Is Valid by Checking . 
        ValidateSessionTime(startTime,endTime);

        // Check On the Session Capacity Is Greater Than Zero and Throw an Exception if It Is Not
        if (capacity <= 0)
            throw new BusinessRuleException("Capacity must be greater than zero.");
    }


    // Validate Session Time by Ensuring That the End Time Is After the Start Time and Throw an Exception if It Is Not
    private static void ValidateSessionTime(DateTime startTime,DateTime endTime)
    {
        if (endTime <= startTime)
            throw new BusinessRuleException("Session end time must be after start time.");
    }


    // Ensure Session Is Editable Or No t By Checking The Status of the Session and Throw an Exception if It Is Not
    private void EnsureSessionIsEditable()
    {
        if (Status == SessionStatus.Completed)
            throw new BusinessRuleException("Completed session cannot be modified.");

        if (Status == SessionStatus.Cancelled)
            throw new BusinessRuleException("Cancelled session cannot be modified.");
    }
}