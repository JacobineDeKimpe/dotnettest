using Xunit.Abstractions;
using Shouldly;
using Rise.Client.Pages;
using Rise.Client.Components;
using Microsoft.AspNetCore.Components;
using Rise.Shared.Users;

namespace Rise.Client.Users;

public class LoginPaginaShould : TestContext
{
    public LoginPaginaShould(ITestOutputHelper outputHelper)
    {
        Services.AddXunitLogger(outputHelper);
        Services.AddScoped<IUserService, FakeUserService>();
    }

    [Fact]
    public void ShowLoginForm()
    {
        var cut = RenderComponent<Login>();

        // Check for username input
        var usernameInput = cut.Find("#username");
        usernameInput.ShouldNotBeNull();

        // Check for password input
        var passwordInput = cut.Find("#password");
        passwordInput.ShouldNotBeNull();

        // Check for submit button
        var submitButton = cut.Find("button[type='submit']");
        submitButton.TextContent.ShouldBe("Inloggen");
    }

    [Fact]
    public void ShowErrorMessageOnInvalidLogin()
    {
        var cut = RenderComponent<Login>();

        // Simulate filling in the login form with invalid credentials
        var usernameInput = cut.Find("#username");
        var passwordInput = cut.Find("#password");
        var submitButton = cut.Find("button[type='submit']");

        usernameInput.Change("wrongUser");
        passwordInput.Change("wrongPassword");
        submitButton.Click();

        // Verify that the error message is shown
        var errorMessage = cut.Find(".error");
        errorMessage.TextContent.ShouldBe("Incorrect username or password. Please try again.");
    }
}