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

        var argsList = new List<string>(args) 
        { 
            "--lang=es-ES",
            "--disable-blink-features=AutomationControlled", // Hides navigator.webdriver
            "--no-sandbox",
            "--disable-setuid-sandbox"
        };

        var browser = await browserType.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = headless,
            Args = argsList
        });

        return await browser.NewContextAsync(new BrowserNewContextOptions
        {
            ViewportSize = new ViewportSize { Width = 1920, Height = 1080 },
            IgnoreHTTPSErrors = true,
            Locale = "es-ES",
            TimezoneId = "Europe/Madrid",
            Geolocation = new Geolocation { Latitude = 40.4168f, Longitude = -3.7038f }, // Madrid
            Permissions = new[] { "geolocation" },
            // Match CI Environment (Linux) to avoid Platform mismatch detection
            UserAgent = "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36"
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
