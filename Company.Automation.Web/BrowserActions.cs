using Microsoft.Playwright;
using Company.Automation.Core.Logging;
using System.Threading.Tasks;

namespace Company.Automation.Web;

public class BrowserActions
{
    private readonly IPage _page;
    private readonly ILoggerService _logger;

    public BrowserActions(IPage page, ILoggerService logger)
    {
        _page = page;
        _logger = logger;
    }

    public async Task ClickAsync(string selector, string description)
    {
        _logger.Info($"Clicking on {description} ({selector})");
        await _page.ClickAsync(selector);
    }

    public async Task TypeAsync(string selector, string text, string description)
    {
        _logger.Info($"Typing '{text}' into {description} ({selector})");
        await _page.FillAsync(selector, text);
    }

    public async Task<string> GetTextAsync(string selector)
    {
         return await _page.InnerTextAsync(selector);
    }

    public async Task<bool> IsVisibleAsync(string selector)
    {
        return await _page.IsVisibleAsync(selector);
    }
}
