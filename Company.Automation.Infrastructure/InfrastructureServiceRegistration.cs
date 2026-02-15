using Company.Automation.Core.Configuration;
using Company.Automation.Core.Logging;
using Company.Automation.Infrastructure.Configuration;
using Company.Automation.Infrastructure.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace Company.Automation.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddSingleton<IConfigurationService, ConfigurationService>();
        services.AddSingleton<ILoggerService, SerilogService>();
        return services;
    }
}
