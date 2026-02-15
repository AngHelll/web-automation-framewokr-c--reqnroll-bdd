using System.Threading.Tasks;
using Company.Automation.Web;
using Microsoft.Playwright;
using Company.Automation.Core.Logging;

namespace AkSolutions.Tests.Components;

public class NavBar
{
    private readonly IPage _page;
    private readonly ILoggerService _logger;

    public NavBar(IPage page, ILoggerService logger)
    {
        _page = page;
        _logger = logger;
    }

    public async Task ClickProjectsAsync()
    {
        await _page.Locator("text=/Proyectos|Projects/i").ClickAsync();
    }

    public async Task ClickNotesAsync()
    {
        await _page.Locator("text=/Ideas|Notes/i").ClickAsync();
    }
}
