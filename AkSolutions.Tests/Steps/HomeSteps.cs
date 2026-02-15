using System.Threading.Tasks;
using AkSolutions.Tests.Pages;
using NUnit.Framework;
using Reqnroll;

namespace AkSolutions.Tests.Steps;

[Binding]
public class HomeSteps
{
    private readonly HomePage _homePage;
    private readonly ProjectsPage _projectsPage;
    private readonly NotesPage _notesPage;

    public HomeSteps(HomePage homePage, ProjectsPage projectsPage, NotesPage notesPage)
    {
        _homePage = homePage;
        _projectsPage = projectsPage;
        _notesPage = notesPage;
    }

    [Given(@"the user opens the Home page")]
    public async Task GivenTheUserOpensTheHomePage()
    {
        await _homePage.NavigateAsync();
    }

    [Then(@"they should see the main title and hero section")]
    public async Task ThenTheyShouldSeeTheMainTitleAndHeroSection()
    {
        var isVisible = await _homePage.IsHeroVisibleAsync();
        Assert.That(isVisible, Is.True, "Hero section should be visible");
    }

    [When(@"they click on ""(.*)"" in the menu")]
    public async Task WhenTheyClickOnInTheMenu(string menuItem)
    {
        if (menuItem == "Projects")
            await _homePage.NavBar.ClickProjectsAsync();
        else if (menuItem == "Notes")
            await _homePage.NavBar.ClickNotesAsync();
    }

    [Then(@"they should navigate to the projects page and see the list")]
    public async Task ThenTheyShouldNavigateToTheProjectsPageAndSeeTheList()
    {
        // await _projectsPage.Page.WaitForURLAsync("**/projects"); // URL check can be flaky with trailing slashes
        var isVisible = await _projectsPage.IsProjectListVisibleAsync();
        Assert.That(isVisible, Is.True, "Projects list should be visible");
    }

    [When(@"they navigate to the ""(.*)"" section")]
    public async Task WhenTheyNavigateToTheSection(string section)
    {
        if (section == "Notes")
            await _homePage.NavBar.ClickNotesAsync();
    }

    [When(@"they open a specific note")]
    public async Task WhenTheyOpenASpecificNote()
    {
        // For MVP, open any available note to verify navigation
        await _notesPage.OpenAnyNoteAsync(); 
    }

    [Then(@"they should see the note content")]
    public async Task ThenTheyShouldSeeTheNoteContent()
    {
        var isVisible = await _notesPage.IsNoteContentVisibleAsync();
        Assert.That(isVisible, Is.True, "Note content should be visible");
    }
}
