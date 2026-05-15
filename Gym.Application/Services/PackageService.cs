namespace Gym.Application.Services;

public sealed class PackageService : IPackageService
{
    // Inject the unit of work to manage database operations related to packages.
    private readonly IUnitOfWork _uow;
    public PackageService(IUnitOfWork uow) => _uow = uow;


}