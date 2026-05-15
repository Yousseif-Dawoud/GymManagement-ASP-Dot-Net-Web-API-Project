namespace Gym.Application.Validators.Members;

public sealed class UpdateMemberRequestValidator
    : AbstractValidator<UpdateMemberRequest>
{
    public UpdateMemberRequestValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty()
            .MaximumLength(150);

        RuleFor(x => x.Phone)
            .NotEmpty()
            .Matches(@"^(01[0125][0-9]{8}|\+201[0125][0-9]{8})$")
            .WithMessage(
                "Phone number must be a valid Egyptian phone number.");

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(200);

        RuleFor(x => x.Gender)
            .IsInEnum()
            .WithMessage("Invalid gender.");

        RuleFor(x => x.DateOfBirth)
            .LessThan(DateOnly.FromDateTime(DateTime.Today))
            .WithMessage("Date of birth must be in the past.");

        RuleFor(x => x.DateOfBirth)
            .Must(BeAtLeast16YearsOld)
            .WithMessage("Member must be at least 16 years old.");

        RuleFor(x => x.EmergencyContact)
            .MaximumLength(20)
            .When(x => !string.IsNullOrWhiteSpace(x.EmergencyContact));

        RuleFor(x => x.MembershipPlanId)
            .GreaterThan(0);

        RuleFor(x => x.PackageId)
            .GreaterThan(0)
            .When(x => x.PackageId.HasValue);

        RuleFor(x => x.MembershipEndDate)
            .GreaterThan(x => x.MembershipStartDate)
            .WithMessage(
                "Membership end date must be after start date.");
    }

    private static bool BeAtLeast16YearsOld(DateOnly dateOfBirth)
    {
        return dateOfBirth <= DateOnly
            .FromDateTime(DateTime.Today.AddYears(-16));
    }
}