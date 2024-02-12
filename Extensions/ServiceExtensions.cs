using LawSchool.Contracts;
using LawSchool.Services;

namespace LawSchool.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureLoggingService(this IServiceCollection services) =>
        services.AddSingleton<ILoggerManager, LoggerManager>();
}