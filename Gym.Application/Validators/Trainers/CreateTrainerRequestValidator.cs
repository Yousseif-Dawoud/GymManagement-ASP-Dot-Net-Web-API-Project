namespace Gym.Application.Validators.Trainers;

public class CreateTrainerRequestValidator : AbstractValidator<CreateTrainerRequest>
{
    public CreateTrainerRequestValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty()
            .MaximumLength(150);

        RuleFor(x => x.Phone)
            .NotEmpty()
            .Matches(@"^(01[0125][0-9]{8}|\+201[0125][0-9]{8})$")
            .WithMessage("Invalid Egyptian phone number.");

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(200);

        RuleFor(x => x.DateOfBirth)
            .LessThan(DateOnly.FromDateTime(DateTime.Today))
            .WithMessage("Date of birth must be in the past.");

        RuleFor(x => x.Specialization)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.ExperienceYears)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Experience years cannot be negative.");

        RuleFor(x => x.HireDate)
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
            .WithMessage("Hire date cannot be in the future.");

        RuleFor(x => x.Bio)
            .MaximumLength(500)
            .When(x => x.Bio != null);
    }
}