using Microsoft.Playwright;
using Company.Automation.Core.Logging;

namespace Company.Automation.Web;

public abstract class BasePage
{
    protected readonly IPage Page;
    protected readonly ILoggerService Logger;

    protected BasePage(IPage page, ILoggerService logger)
    {
        Page = page;
        Logger = logger;
    }

    public abstract string PagePath { get; }

    public async Task NavigateAsync()
    {
        Logger.Info($"Navigating to {PagePath}");
        await Page.GotoAsync(PagePath);
    }
    
    public async Task<bool> IsVisibleAsync()
    {
        // Simple default check. Can be overridden.
        return await Page.WaitForLoadStateAsync(LoadState.NetworkIdle).ContinueWith(_ => true);
    }
}
