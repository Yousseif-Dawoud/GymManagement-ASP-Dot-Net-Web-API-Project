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
            .MaximumLength(20);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.MembershipPlanId)
            .GreaterThan(0);

        RuleFor(x => x.MembershipEndDate)
            .GreaterThanOrEqualTo(x => x.MembershipStartDate)
            .WithMessage("Membership end date must be after start date.");
    }
}