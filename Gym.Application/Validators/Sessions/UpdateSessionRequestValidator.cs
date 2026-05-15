using FluentValidation;
using Gym.Application.DTOs.Sessions;

namespace Gym.Application.Validators.Sessions;

public sealed class UpdateSessionRequestValidator : AbstractValidator<UpdateSessionRequest>
{
    public UpdateSessionRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(150);

        RuleFor(x => x.Description)
            .MaximumLength(1000);

        RuleFor(x => x.Type)
            .IsInEnum();

        RuleFor(x => x.Capacity)
            .GreaterThan(0);

        RuleFor(x => x.TrainerId)
            .GreaterThan(0);

        RuleFor(x => x.EndTime)
            .GreaterThan(x => x.StartTime)
            .WithMessage("Session end time must be after start time.");

        RuleFor(x => x.Capacity)
            .Equal(1)
            .When(x => x.Type == SessionType.Personal)
            .WithMessage("Personal session capacity must be exactly 1.");
    }
}