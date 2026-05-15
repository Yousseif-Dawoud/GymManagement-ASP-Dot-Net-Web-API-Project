namespace Gym.Application.Validators.Trainers;

public class UpdateTrainerRequestValidator : AbstractValidator<UpdateTrainerRequest>
{
    public UpdateTrainerRequestValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty()
            .MaximumLength(150);

        RuleFor(x => x.Phone)
            .NotEmpty()
            .Matches(@"^(01[0125][0-9]{8}|\+201[0125][0-9]{8})$");

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Specialization)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.ExperienceYears)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.HireDate)
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today));

        RuleFor(x => x.Bio)
            .MaximumLength(500)
            .When(x => x.Bio != null);
    }
}