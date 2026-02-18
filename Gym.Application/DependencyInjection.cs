
namespace Gym.Application;

public static class DependencyInjection
{
    // This Method is an extension method for the IServiceCollection interface

    // This method registers the application services with the dependency injection container.
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IMemberService, MemberService>();
        services.AddScoped<ITrainerService, TrainerService>();
        services.AddScoped<IMembershipPlanService, MembershipPlanService>();
        services.AddScoped<ISessionService, SessionService>();
        return services;
    }
}
