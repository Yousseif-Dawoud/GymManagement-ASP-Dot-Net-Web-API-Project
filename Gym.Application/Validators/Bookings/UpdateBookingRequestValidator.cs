namespace Gym.Application.Validators.Bookings;

public sealed class UpdateBookingRequestValidator : AbstractValidator<UpdateBookingRequest>
{
    public UpdateBookingRequestValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Booking Id must be greater than zero.");

        RuleFor(x => x.Notes)
            .MaximumLength(500)
            .WithMessage("Notes cannot exceed 500 characters.");
    }
}