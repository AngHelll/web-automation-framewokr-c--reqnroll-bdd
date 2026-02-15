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
        await _page.ClickAsync("text=Proyectos"); // Adjust selector as needed based on actual site
    }

    public async Task ClickNotesAsync()
    {
        // The UI label is "Ideas" but conceptually it's the Notes/Blog section
        await _page.ClickAsync("text=Ideas"); 
    }
}
