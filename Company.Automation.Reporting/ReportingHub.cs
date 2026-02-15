using System.Collections.Generic;
using System.Linq;

namespace Company.Automation.Reporting;

public class ReportingHub : IReportingService
{
    private readonly IEnumerable<IReportProvider> _providers;

    public ReportingHub(IEnumerable<IReportProvider> providers)
    {
        _providers = providers;
    }

    public void StartTest(string testName, string description = "")
    {
        foreach (var provider in _providers)
            provider.OnTestStarted(testName, description);
    }

    public void EndTest(string status, string message = "")
    {
        foreach (var provider in _providers)
             provider.OnTestFinished(status, message);
    }

    public void Log(string message, string level = "INFO")
    {
        foreach (var provider in _providers)
            provider.OnLog(message, level);
    }

    public void AddAttachment(string name, string type, byte[] content)
    {
        foreach (var provider in _providers)
            provider.OnAttachmentAdded(name, type, content);
    }
}
