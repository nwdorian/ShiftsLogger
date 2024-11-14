using ShiftsLogger.ConsoleUI.Menus;
using ShiftsLogger.ConsoleUI.Services;

namespace Microsoft.Extensions.DependencyInjection;
public static class ConsoleServiceExtensions
{
    public static IServiceCollection AddMenuServices(this IServiceCollection services)
    {
        services.AddScoped<MainMenu>();
        services.AddScoped<UsersMenu>();
        services.AddScoped<ShiftsMenu>();
        return services;
    }

    public static IServiceCollection AddConsoleServices(this IServiceCollection services)
    {
        services.AddScoped<UserService>();
        return services;
    }
}
