namespace Gym.Application.DTOs.Members;

using System.ComponentModel.DataAnnotations;

public sealed record UpdateMemberRequest(
    [param: Required, MaxLength(120)] string FullName,
    [param: Required, Phone, MaxLength(25)] string Phone,
    [param: Required, EmailAddress, MaxLength(200)] string Email,
    DateOnly MembershipStartDate,
    DateOnly MembershipEndDate,
    [param: Range(1, int.MaxValue)] int MembershipPlanId
) : IValidatableObject
{
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (MembershipEndDate < MembershipStartDate)
        {
            yield return new ValidationResult(
                "MembershipEndDate must be greater than or equal to MembershipStartDate.",
                new[] { nameof(MembershipStartDate), nameof(MembershipEndDate) });
        }
    }
}
