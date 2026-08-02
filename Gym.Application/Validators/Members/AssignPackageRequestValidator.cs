public sealed class AssignPackageRequestValidator
    : AbstractValidator<AssignPackageRequest>
{
    public AssignPackageRequestValidator()
    {
        RuleFor(x => x.PackageId)
            .GreaterThan(0)
            .WithMessage("Package is required.");
    }
}