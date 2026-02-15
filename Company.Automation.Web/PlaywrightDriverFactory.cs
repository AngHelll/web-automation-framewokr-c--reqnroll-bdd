using Microsoft.Playwright;
using System.Threading.Tasks;
using Company.Automation.Core.Configuration;

namespace Company.Automation.Web;

public interface IDriverFactory
{
    Task<IBrowserContext> CreateContextAsync();
    Task<IPage> CreatePageAsync(IBrowserContext context);
    Task CloseAsync(IBrowserContext context);
}

public class PlaywrightDriverFactory : IDriverFactory
{
    private readonly IConfigurationService _configuration;

    public PlaywrightDriverFactory(IConfigurationService configuration)
    {
        _configuration = configuration;
    }

    public async Task<IBrowserContext> CreateContextAsync()
    {
        var playwright = await Playwright.CreateAsync();
        var browserType = _configuration.GetString("Browser:Type")?.ToLower() switch
        {
            "firefox" => playwright.Firefox,
            "webkit" => playwright.Webkit,
            _ => playwright.Chromium
        };

        var headless = _configuration.GetBool("Browser:Headless", true);
        var args = _configuration.Get<string[]>("Browser:Args") ?? new string[] { };

        var browser = await browserType.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = headless,
            Args = args
        });

        return await browser.NewContextAsync(new BrowserNewContextOptions
        {
            ViewportSize = new ViewportSize { Width = 1920, Height = 1080 },
            IgnoreHTTPSErrors = true,
            Locale = "es-ES" // Force Spanish locale to match test selectors
        });
    }

    public async Task<IPage> CreatePageAsync(IBrowserContext context)
    {
        return await context.NewPageAsync();
    }

    public async Task CloseAsync(IBrowserContext context)
    {
        if (context != null)
        {
            await context.CloseAsync();
            if (context.Browser != null)
                await context.Browser.CloseAsync();
        }
    }
}
