using LawSchool.Contracts;
using LawSchool.Data;
using LawSchool.Services;

namespace LawSchool.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureLoggingService(this IServiceCollection services) =>
        services.AddSingleton<ILoggerManager, LoggerManager>();

    public static void ConfigureServiceManager(this IServiceCollection services) =>
       services.AddScoped<IServiceManager, ServiceManager>();

    public static void ConfigureRepositoryManager(this IServiceCollection services) =>
        services.AddScoped<IRepositoryManager, RepositoryManager>();
}