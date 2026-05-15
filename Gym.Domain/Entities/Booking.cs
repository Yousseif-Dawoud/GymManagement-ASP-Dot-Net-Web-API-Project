namespace Gym.Domain.Entities;

public class Booking : BaseEntity
{
    // Booking Information
    // =========================
    public DateTime BookingDate { get; private set; }
    public BookingStatus Status { get; private set; }
    public string? Notes { get; private set; }


    // Relationships
    // =========================

    // Member
    public int MemberId { get; private set; }
    public Member Member { get; private set; } = null!;

    // Session
    public int SessionId { get; private set; }
    public Session Session { get; private set; } = null!;


    // EF Core Constructor
    // =========================
    private Booking() { }


    // Main Constructor
    // =========================
    public Booking(int memberId,int sessionId,string? notes = null)
    {
        Validate(memberId, sessionId);

        MemberId = memberId;
        SessionId = sessionId;

        Notes = notes?.Trim();

        BookingDate = DateTime.UtcNow;

        // First status in lifecycle
        Status = BookingStatus.Pending;
    }


    // Booking Behaviors
    // =========================

    public void Confirm()
    {
        if (Status != BookingStatus.Pending)
            throw new BusinessRuleException("Only pending bookings can be confirmed.");

        Status = BookingStatus.Confirmed;

        SetUpdated();
    }


    public void Cancel()
    {
        if (Status == BookingStatus.Cancelled)
            return;

        if (Status == BookingStatus.Attended)
            throw new BusinessRuleException("Cannot cancel an attended booking.");

        Status = BookingStatus.Cancelled;

        SetUpdated();
    }


    public void MarkAsAttended()
    {
        if (Status != BookingStatus.Confirmed)
            throw new BusinessRuleException("Only confirmed bookings can be marked as attended.");

        Status = BookingStatus.Attended;

        SetUpdated();
    }


    public void MarkAsMissed()
    {
        if (Status != BookingStatus.Confirmed)
            throw new BusinessRuleException("Only confirmed bookings can be marked as missed.");

        Status = BookingStatus.Missed;

        SetUpdated();
    }


    public void UpdateNotes(string? notes)
    {
        Notes = notes?.Trim();

        SetUpdated();
    }


    // Validation
    // =========================
    private static void Validate(int memberId,int sessionId)
    {
        if (memberId <= 0)  throw new BusinessRuleException("Member id is invalid.");

        if (sessionId <= 0) throw new BusinessRuleException("Session id is invalid.");
    }
}