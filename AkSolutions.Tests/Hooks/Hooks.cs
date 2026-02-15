using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Playwright;
using Reqnroll;
using Reqnroll.BoDi;
using Company.Automation.Core.Logging;
using Company.Automation.Reporting;
using Company.Automation.Web;
using Company.Automation.Infrastructure;
using Company.Automation.Reporting.Providers;
using AkSolutions.Tests.Pages;

namespace AkSolutions.Tests.Hooks;

[Binding]
public class Hooks
{
    private readonly IObjectContainer _objectContainer;
    private static IServiceProvider? _serviceProvider;
    private static readonly object _lock = new object();
    private IBrowserContext? _context;
    private IPage? _page;

    public Hooks(IObjectContainer objectContainer)
    {
        _objectContainer = objectContainer;
        NUnit.Framework.TestContext.Progress.WriteLine("[Hooks] Constructor Executed");
    }

    private static void EnsureGlobalSetup()
    {
        if (_serviceProvider != null) return;
        
        lock (_lock)
        {
            if (_serviceProvider != null) return;

            try
            {
                NUnit.Framework.TestContext.Progress.WriteLine("[Hooks] Initializing Global Services...");
                var services = new ServiceCollection();
                services.AddInfrastructureServices();
                services.AddSingleton<IDriverFactory, PlaywrightDriverFactory>();
                services.AddSingleton<IReportingService>(sp => 
                {
                    return new ReportingHub(new IReportProvider[] { new ConsoleReportProvider() });
                });

                _serviceProvider = services.BuildServiceProvider();
                NUnit.Framework.TestContext.Progress.WriteLine("[Hooks] Global Services Initialized.");
            }
            catch (Exception ex)
            {
                NUnit.Framework.TestContext.Progress.WriteLine($"[Hooks] Global Initialization FAILED: {ex}");
                throw;
            }
        }
    }

    [BeforeScenario]
    public async Task Setup()
    {
        try 
        {
            EnsureGlobalSetup();
            
            if (_serviceProvider == null) throw new Exception("ServiceProvider is null after initialization.");

            NUnit.Framework.TestContext.Progress.WriteLine("[Hooks] Scenario Setup started.");

            var driverFactory = _serviceProvider.GetRequiredService<IDriverFactory>();
            var logger = _serviceProvider.GetRequiredService<ILoggerService>();
            var reporting = _serviceProvider.GetRequiredService<IReportingService>();

            _context = await driverFactory.CreateContextAsync();
            _page = await driverFactory.CreatePageAsync(_context);

            // Register Page Objects for DI injection
            _objectContainer.RegisterInstanceAs(_page);
            _objectContainer.RegisterInstanceAs(logger);
            _objectContainer.RegisterInstanceAs(reporting);
            
            // Register Pages
            _objectContainer.RegisterInstanceAs(new HomePage(_page, logger));
            _objectContainer.RegisterInstanceAs(new ProjectsPage(_page, logger));
            _objectContainer.RegisterInstanceAs(new NotesPage(_page, logger));

            // Start Reporting
            // Resolve ScenarioContext via DI (Reqnroll standard)
            var scenarioContext = _objectContainer.Resolve<ScenarioContext>();
            reporting.StartTest(scenarioContext.ScenarioInfo.Title, scenarioContext.ScenarioInfo.Description);
            
            NUnit.Framework.TestContext.Progress.WriteLine("[Hooks] Scenario Setup completed.");
        }
        catch (Exception ex)
        {
             NUnit.Framework.TestContext.Progress.WriteLine($"[Hooks] Setup FAILED: {ex}");
             throw;
        }
    }

    [AfterScenario]
    public async Task TearDown()
    {
        if (_serviceProvider == null) return;

        var driverFactory = _serviceProvider.GetRequiredService<IDriverFactory>();
        var reporting = _serviceProvider.GetRequiredService<IReportingService>();
        
        try
        {
            var scenarioContext = _objectContainer.Resolve<ScenarioContext>();
            var status = scenarioContext.TestError == null ? "PASSED" : "FAILED";
            var message = scenarioContext.TestError?.Message ?? "";

            if (scenarioContext.TestError != null && _page != null)
            {
                 var screenshot = await _page.ScreenshotAsync(new PageScreenshotOptions { FullPage = true });
                 reporting.AddAttachment("Failure Screenshot", "image/png", screenshot);
            }

            reporting.EndTest(status, message);
        }
        catch(Exception ex)
        {
            NUnit.Framework.TestContext.Progress.WriteLine($"[Hooks] TearDown Reporting Error: {ex}");
        }

        if (_context != null)
        {
             await driverFactory.CloseAsync(_context);
        }
    }
}
