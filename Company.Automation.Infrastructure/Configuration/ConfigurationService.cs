using System;
using Microsoft.Extensions.Configuration;
using System.IO;
using Company.Automation.Core.Configuration;

namespace Company.Automation.Infrastructure.Configuration;

public class ConfigurationService : IConfigurationService
{
    private readonly IConfiguration _config;

    public ConfigurationService()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
            .AddEnvironmentVariables();

        _config = builder.Build();
    }

    public string? GetString(string key)
    {
        return _config[key];
    }

    public int GetInt(string key, int defaultValue = 0)
    {
        var val = _config[key];
        return int.TryParse(val, out var result) ? result : defaultValue;
    }

    public bool GetBool(string key, bool defaultValue = false)
    {
        var val = _config[key];
        return bool.TryParse(val, out var result) ? result : defaultValue;
    }

    public T? Get<T>(string key)
    {
        return _config.GetSection(key).Get<T>();
    }
}
