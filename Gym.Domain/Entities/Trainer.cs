namespace Gym.Domain.Entities;

public class Trainer : BaseEntity
{
    // Personal Information
    // =========================
    public string FullName { get; private set; } = null!;
    public string Phone { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public Gender Gender { get; private set; }
    public DateOnly DateOfBirth { get; private set; }


    // Professional Information
    // =========================
    public string Specialization { get; private set; } = null!;
    public int ExperienceYears { get; private set; }
    public DateOnly HireDate { get; private set; }
    public string? Bio { get; private set; }


    // Status
    // =========================
    public TrainerStatus Status { get; private set; }


    // Navigation Properties
    // =========================
    private readonly List<Session> _sessions = new();
    public IReadOnlyCollection<Session> Sessions => _sessions;


    // EF Core Constructor
    // =========================
    private Trainer() { }


    // Main Constructor
    // =========================
    public Trainer(
        string fullName,
        string phone,
        string email,
        Gender gender,
        DateOnly dateOfBirth,
        string specialization,
        int experienceYears,
        DateOnly hireDate,
        string? bio = null)
    {
        Validate(fullName,phone,email,dateOfBirth,specialization,experienceYears,hireDate);

        FullName = fullName.Trim();
        Phone = phone.Trim();
        Email = email.Trim().ToLowerInvariant();

        Gender = gender;
        DateOfBirth = dateOfBirth;

        Specialization = specialization.Trim();
        ExperienceYears = experienceYears;
        HireDate = hireDate;

        Bio = bio?.Trim();

        Status = TrainerStatus.Active;
    }


    // Update Trainer Information
    // =========================

    public void Update(
        string fullName,
        string phone,
        string email,
        Gender gender,
        DateOnly dateOfBirth,
        string specialization,
        int experienceYears,
        DateOnly hireDate,
        string? bio = null)
    {
        Validate(fullName, phone, email, dateOfBirth, specialization, experienceYears, hireDate);

        FullName = fullName.Trim();
        Phone = phone.Trim();
        Email = email.Trim().ToLowerInvariant();

        Gender = gender;
        DateOfBirth = dateOfBirth;

        Specialization = specialization.Trim();
        ExperienceYears = experienceYears;
        HireDate = hireDate;

        Bio = bio?.Trim();

        SetUpdated();
    }


    // Status Behaviors
    // =========================

    public void Activate()
    {
        if (Status == TrainerStatus.Active)
            return;

        Status = TrainerStatus.Active;

        SetUpdated();
    }

    public void Deactivate()
    {
        if (Status == TrainerStatus.Inactive)
            return;

        Status = TrainerStatus.Inactive;

        SetUpdated();
    }

    public void PutOnLeave()
    {
        if (Status == TrainerStatus.OnLeave)
            return;

        Status = TrainerStatus.OnLeave;

        SetUpdated();
    }


    // Professional Behaviors
    // =========================

    public void ChangeSpecialization(string specialization)
    {
        if (string.IsNullOrWhiteSpace(specialization))
            throw new BusinessRuleException("Specialization is required.");

        Specialization = specialization.Trim();

        SetUpdated();
    }

    public void UpdateBio(string? bio)
    {
        Bio = bio?.Trim();

        SetUpdated();
    }

    public void UpdateExperience(int experienceYears)
    {
        if (experienceYears < 0)
            throw new BusinessRuleException("Experience years cannot be negative.");

        ExperienceYears = experienceYears;

        SetUpdated();
    }


    // Validation
    // =========================

    private static void Validate(string fullName,string phone,string email,DateOnly dateOfBirth,string specialization,int experienceYears,DateOnly hireDate)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            throw new BusinessRuleException("Full name is required.");

        if (string.IsNullOrWhiteSpace(phone))
            throw new BusinessRuleException("Phone number is required.");

        if (string.IsNullOrWhiteSpace(email))
            throw new BusinessRuleException("Email is required.");

        if (string.IsNullOrWhiteSpace(specialization))
            throw new BusinessRuleException("Specialization is required.");

        if (experienceYears < 0)
            throw new BusinessRuleException("Experience years cannot be negative.");

        if (hireDate > DateOnly.FromDateTime(DateTime.Today))
            throw new BusinessRuleException("Hire date cannot be in the future.");

        if (dateOfBirth >= DateOnly.FromDateTime(DateTime.Today))
            throw new BusinessRuleException("Date of birth must be in the past.");
    }
}