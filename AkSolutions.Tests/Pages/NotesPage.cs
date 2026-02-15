using System.Threading.Tasks;
using Company.Automation.Web;
using Company.Automation.Core.Logging;
using Microsoft.Playwright;

namespace AkSolutions.Tests.Pages;

public class NotesPage : BasePage
{
    public NotesPage(IPage page, ILoggerService logger) : base(page, logger)
    {
    }

    public override string PagePath => "https://ak-solutions.app/notes"; // Verify URL

    public async Task OpenNoteAsync(string noteTitle)
    {
        await Page.ClickAsync($"text={noteTitle}");
    }

    public async Task OpenAnyNoteAsync()
    {
        // Click the first link that looks like a note
        // Using Locator to find all matches and click the first one
        await Page.Locator("a[href*='/notas/']").First.ClickAsync();
    }

    public async Task<bool> IsNoteContentVisibleAsync()
    {
        try
        {
             // Check for at least one content indicator
             await Page.WaitForSelectorAsync("h1, article", new PageWaitForSelectorOptions { State = WaitForSelectorState.Visible });
             return true;
        }
        catch
        {
             return false;
        }
    }
}
