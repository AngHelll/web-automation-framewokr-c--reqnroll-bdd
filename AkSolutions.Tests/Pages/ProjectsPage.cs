using System.Threading.Tasks;
using Company.Automation.Web;
using Company.Automation.Core.Logging;
using Microsoft.Playwright;

namespace AkSolutions.Tests.Pages;

public class ProjectsPage : BasePage
{
    public ProjectsPage(IPage page, ILoggerService logger) : base(page, logger)
    {
    }

    public override string PagePath => "https://ak-solutions.app/projects"; // Verify URL

    public async Task<bool> IsProjectListVisibleAsync()
    {
        try
        {
             await Page.WaitForSelectorAsync("h1:has-text('Proyectos')", new PageWaitForSelectorOptions { State = WaitForSelectorState.Visible });
             return true;
        }
        catch
        {
             return false;
        }
    }
}
