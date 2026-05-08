namespace Gym.Application.Validators.MembershipPlans;

public sealed class CreateMembershipPlanRequestValidator
    : AbstractValidator<CreateMembershipPlanRequest>
{
    public CreateMembershipPlanRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Price)
            .GreaterThan(0);

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(500);

        RuleFor(x => x.MaxSessionsPerMonth)
            .GreaterThan(0);

        RuleFor(x => x.Type)
            .IsInEnum();

        RuleFor(x => x.IncludesPersonalTrainer)
            .NotNull();
    }
}