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
             // Check if we are stuck on Cloudflare WAF
             var title = await Page.TitleAsync();
             if (title.Contains("Un momento") || title.Contains("Just a moment"))
             {
                 Logger.Warning("WAF Detected. Waiting additional time for clearance...");
                 await Page.WaitForTimeoutAsync(5000); // Give it a moment to clear
             }

             // Check for specific text to ensure we are on the right page
             await Page.WaitForSelectorAsync("h1:has-text('Angel')", new PageWaitForSelectorOptions { State = WaitForSelectorState.Visible, Timeout = 30000 });
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
