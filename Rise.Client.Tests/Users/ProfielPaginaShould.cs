using Rise.Shared.Users;
using Xunit.Abstractions;
using Shouldly;
using System.Collections.Generic;
using Rise.Client.Pages;
using Rise.Client.Components;

namespace Rise.Client.Users;
public class ProfielPaginaShould : TestContext
{
    public ProfielPaginaShould(ITestOutputHelper outputHelper)
    {
        Services.AddXunitLogger(outputHelper);
        Services.AddScoped<IUserService, FakeUserService>();
    }

    [Fact]
    public void ShowHeaderInfo()
    {
        var cut = RenderComponent<ProfielPagina>();

        var headerProfielComponent = cut.FindComponent<HeaderProfiel>();

        var nameElement = headerProfielComponent.Find("h1");
        nameElement.MarkupMatches("<h1>Welkom, Voornaam 1!</h1>");

        var roleElement = headerProfielComponent.Find("p");
        roleElement.TextContent.ShouldBe("ADMIN");

    }

    [Fact]
    public void ShowGebruikerInfo()
    {
        var cut = RenderComponent<ProfielPagina>();

        var gebruikerinfocomponent = cut.FindComponent<GebruikerInfo>();

        gebruikerinfocomponent.Find("p:nth-child(1)").MarkupMatches("<p>Naam: Naam 1</p>");

        gebruikerinfocomponent.Find("p:nth-child(2)").MarkupMatches("<p>Email: user1@example.com</p>");

        gebruikerinfocomponent.Find("p:nth-child(3)").MarkupMatches("<p>TelefoonNr: 12345</p>");

        gebruikerinfocomponent.Find("button").TextContent.ShouldBe("Wijzig");


    }
}