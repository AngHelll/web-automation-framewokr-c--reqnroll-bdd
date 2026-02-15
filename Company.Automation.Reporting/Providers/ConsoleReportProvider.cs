using System;

namespace Company.Automation.Reporting.Providers;

public class ConsoleReportProvider : IReportProvider
{
    public void OnTestStarted(string testName, string description)
    {
        Console.WriteLine($"[REPORT] Test Started: {testName} - {description}");
    }

    public void OnTestFinished(string status, string message)
    {
        Console.WriteLine($"[REPORT] Test Finished: {status} - {message}");
    }

    public void OnLog(string message, string level)
    {
         Console.WriteLine($"[REPORT] [{level}] {message}");
    }

    public void OnAttachmentAdded(string name, string type, byte[] content)
    {
         Console.WriteLine($"[REPORT] Attachment Added: {name} ({type})");
    }
}
