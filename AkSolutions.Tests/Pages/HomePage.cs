using System.Threading.Tasks;
using Company.Automation.Web;
using Company.Automation.Core.Logging;
using Microsoft.Playwright;
using AkSolutions.Tests.Components;

namespace AkSolutions.Tests.Pages;

public class HomePage : BasePage
{
    public readonly NavBar NavBar;

    public HomePage(IPage page, ILoggerService logger) : base(page, logger)
    {
        NavBar = new NavBar(page, logger);
    }

    public override string PagePath => "https://ak-solutions.app";

    public async Task<bool> IsHeroVisibleAsync()
    {
        try
        {
             await Page.WaitForSelectorAsync("h1", new PageWaitForSelectorOptions { State = WaitForSelectorState.Visible });
             return true;
        }
        catch
        {
             return false;
        }
    }

    public async Task<string> GetHeadlineAsync()
    {
        return await Page.InnerTextAsync("h1");
    }
}
