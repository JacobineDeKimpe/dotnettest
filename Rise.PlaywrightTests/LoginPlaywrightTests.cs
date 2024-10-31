using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace Rise.Client.Pages;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class LoginShould : PageTest
{
    [Test]
    public async Task LoginSuccessfully()
    {
        await Page.GotoAsync("https://localhost:5001/login");

        // Fill in the login form
        await Page.FillAsync("#username", "admin");
        await Page.FillAsync("#password", "password");

        // Submit the form
        await Page.ClickAsync("button[type='submit']");

        // Verify successful login (e.g., welcome message)
        var welcomeMessage = await Page.TextContentAsync(".welcome-message");
        Assert.AreEqual("Welkom admin!", welcomeMessage);
    }

    [Test]
    public async Task ShowsErrorMessageOnInvalidLogin()
    {
        await Page.GotoAsync("http://localhost:5001/login");

        // Fill in the login form with incorrect credentials
        await Page.FillAsync("#username", "wrongUser");
        await Page.FillAsync("#password", "wrongPassword");

        // Submit the form
        await Page.ClickAsync("button[type='submit']");

        // Check if the error message is displayed
        var errorMessage = await Page.TextContentAsync(".error");
        Assert.AreEqual("Incorrect username or password. Please try again.", errorMessage);
    }
}