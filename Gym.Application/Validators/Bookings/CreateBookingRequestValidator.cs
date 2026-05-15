namespace Gym.Application.Validators.Bookings;

public sealed class CreateBookingRequestValidator : AbstractValidator<CreateBookingRequest>
{
    public CreateBookingRequestValidator()
    {
        RuleFor(x => x.MemberId)
            .GreaterThan(0)
            .WithMessage("Member Id must be greater than zero.");

        RuleFor(x => x.SessionId)
            .GreaterThan(0)
            .WithMessage("Session Id must be greater than zero.");

        RuleFor(x => x.Notes)
            .MaximumLength(500)
            .WithMessage("Notes cannot exceed 500 characters.");
    }
}