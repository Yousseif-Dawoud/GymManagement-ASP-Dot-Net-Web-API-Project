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
    }
}