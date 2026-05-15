namespace Gym.Application.Validators.MembershipPlans;

public sealed class UpdateMembershipPlanRequestValidator
    : AbstractValidator<UpdateMembershipPlanRequest>
{
    public UpdateMembershipPlanRequestValidator()
    {
        // Type
        // =========================
        RuleFor(x => x.Type)
            .IsInEnum()
            .WithMessage("Invalid membership plan type.");


        // Price
        // =========================
        RuleFor(x => x.Price)
            .GreaterThan(0)
            .WithMessage("Price must be greater than zero.");


        // Description
        // =========================

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Description is required.")
            .MaximumLength(500)
            .WithMessage("Description cannot exceed 500 characters.");


        // Duration
        // =========================
        RuleFor(x => x.DurationInDays)
            .GreaterThan(0)
            .WithMessage("Duration must be greater than zero.");


        // Max Sessions
        // =========================
        RuleFor(x => x.MaxSessionsPerMonth)
            .GreaterThan(0)
            .WithMessage("Max sessions per month must be greater than zero.");


        // VIP Rule
        // =========================
        RuleFor(x => x.IncludesPersonalTrainer)
            .Equal(true)
            .When(x => x.Type == MembershipPlanType.VIP)
            .WithMessage("VIP plans must include a personal trainer.");
    }
}