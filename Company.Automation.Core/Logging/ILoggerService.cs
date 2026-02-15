using System;
using System.Collections.Generic;

namespace Company.Automation.Core.Logging;

public interface ILoggerService
{
    void Debug(string message, Dictionary<string, object>? properties = null);
    void Info(string message, Dictionary<string, object>? properties = null);
    void Warning(string message, Dictionary<string, object>? properties = null);
    void Error(string message, Exception? ex = null, Dictionary<string, object>? properties = null);
    void Fatal(string message, Exception? ex = null, Dictionary<string, object>? properties = null);
}
