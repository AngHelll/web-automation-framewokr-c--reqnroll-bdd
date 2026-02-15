using System.Collections.Generic;

namespace Company.Automation.Reporting;

public interface IReportingService
{
    void StartTest(string testName, string description = "");
    void EndTest(string status, string message = "");
    void Log(string message, string level = "INFO");
    void AddAttachment(string name, string type, byte[] content);
}

public interface IReportProvider
{
    void OnTestStarted(string testName, string description);
    void OnTestFinished(string status, string message);
    void OnLog(string message, string level);
    void OnAttachmentAdded(string name, string type, byte[] content);
}
