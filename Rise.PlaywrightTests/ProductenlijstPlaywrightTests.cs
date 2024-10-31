using System;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace Rise.Client.Pages;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class ProductenlijstPlaywrightTests : PageTest
{
    [Test]
    public async Task ProductZoeken_Should_GefilterdeResultatenTonen_KaartView()
    {
        await Page.GotoAsync("https://localhost:5001");

        await Page.FillAsync("#product-search", "Product 1");
        await Page.ClickAsync("button:has-text('Zoek')");

        var productKaarten = await Page.QuerySelectorAllAsync("div.product-card");

        var allMatch = true;

        foreach (var kaart in productKaarten)
        {
            var productNaam = await kaart.InnerTextAsync();
            if (!productNaam.Contains("Product 1", StringComparison.OrdinalIgnoreCase))
            {
                allMatch = false;
                break;
            }
        }

        Assert.True(allMatch, "Ze bevatten allemaal de geschreven zoekopdracht.");
    }

    [Test]
    public async Task ProductZoeken_Should_GefilterdeResultatenTonen_TableView()
    {
        await Page.GotoAsync("https://localhost:5001");
        await Page.ClickAsync("i"); // Switch to table view

        await Page.FillAsync("#product-search", "Product 1");
        await Page.ClickAsync("button:has-text('Zoek')");

        await Page.WaitForSelectorAsync(".product-table");

        var productRijen = await Page.QuerySelectorAllAsync(".product-table > tbody > tr");

        var allMatch = true;

        foreach (var rij in productRijen)
        {
            var naamCell = await rij.QuerySelectorAsync("td:nth-child(1)");
            if (naamCell != null)
            {
                var naamTekst = await naamCell.InnerTextAsync();
                if (!naamTekst.Contains("Product 1", StringComparison.OrdinalIgnoreCase))
                {
                    allMatch = false;
                    break;
                }
            }
        }

        Assert.True(allMatch, "Ze bevatten allemaal de geschreven zoekopdracht.");
    }

    [Test]
    public async Task ToggleView_Should_NaarTableViewWisselen()
    {
        await Page.GotoAsync("https://localhost:5001");
        await Page.ClickAsync("i"); // Switch to table view

        bool isTabelView = await Page.IsVisibleAsync("table.product-table");
        Assert.True(isTabelView, "De tabelweergave zou zichtbaar moeten zijn.");
    }

    [Test]
    public async Task ToggleView_Should_NaarTableViewWisselen_DanTerugKaartView()
    {
        await Page.GotoAsync("https://localhost:5001");
        await Page.ClickAsync("i"); // Switch to table view

        bool isTabelView = await Page.IsVisibleAsync("table.product-table");
        Assert.True(isTabelView, "De tabelweergave zou zichtbaar moeten zijn.");

        await Page.ClickAsync("i"); // Switch terug naar kaartweergave

        bool isKaartView = await Page.IsVisibleAsync(".product-list");
        Assert.True(isKaartView, "De kaartweergave zou zichtbaar moeten zijn.");
    }
}
