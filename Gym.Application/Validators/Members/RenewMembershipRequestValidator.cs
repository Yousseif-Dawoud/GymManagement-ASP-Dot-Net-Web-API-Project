namespace Gym.Application.Validators.Members;

public sealed class RenewMembershipRequestValidator : AbstractValidator<RenewMembershipRequest>
{
    public RenewMembershipRequestValidator()
    {
        RuleFor(x => x.MembershipPlanId)
            .GreaterThan(0)
            .WithMessage("Membership plan is required.");

        RuleFor(x => x.EndDate)
            .GreaterThan(x => x.StartDate)
            .WithMessage("Membership end date must be after start date.");

        RuleFor(x => x.StartDate)
             .NotEmpty()
             .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
             .WithMessage("Membership start date cannot be in the past.");
    }
}