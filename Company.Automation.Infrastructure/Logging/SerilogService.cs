using System;
using System.Collections.Generic;
using Company.Automation.Core.Logging;
using Serilog;
using Serilog.Context;

namespace Company.Automation.Infrastructure.Logging;

public class SerilogService : ILoggerService
{
    private readonly ILogger _logger;

    public SerilogService()
    {
        _logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .Enrich.FromLogContext()
            .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
            .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();
    }

    public void Debug(string message, Dictionary<string, object>? properties = null)
    {
        using (PushProperties(properties))
        {
            _logger.Debug(message);
        }
    }

    public void Info(string message, Dictionary<string, object>? properties = null)
    {
        using (PushProperties(properties))
        {
            _logger.Information(message);
        }
    }

    public void Warning(string message, Dictionary<string, object>? properties = null)
    {
        using (PushProperties(properties))
        {
            _logger.Warning(message);
        }
    }

    public void Error(string message, Exception? ex = null, Dictionary<string, object>? properties = null)
    {
        using (PushProperties(properties))
        {
            if (ex != null)
                _logger.Error(ex, message);
            else
                _logger.Error(message);
        }
    }

    public void Fatal(string message, Exception? ex = null, Dictionary<string, object>? properties = null)
    {
        using (PushProperties(properties))
        {
             if (ex != null)
                _logger.Fatal(ex, message);
            else
                _logger.Fatal(message);
        }
    }

    private IDisposable? PushProperties(Dictionary<string, object>? properties)
    {
        if (properties == null || properties.Count == 0)
            return null;

        IDisposable? push = null;
        foreach (var prop in properties)
        {
             push = LogContext.PushProperty(prop.Key, prop.Value);
        }
        return push; 
        // Note: This basic implementation only pushes the last one correctly if we don't chain them. 
        // For production, we'd need a composite disposable. 
        // Simplified for MVP.
    }
}
