using Rise.Shared.Products;
using Xunit.Abstractions;
using Shouldly;
using System.Collections.Generic;
using Rise.Client.Pages;

namespace Rise.Client.Products;

/// <summary>
/// These tests are written entirely in C#.
/// Learn more at https://bunit.dev/docs/getting-started/writing-tests.html#creating-basic-tests-in-cs-files
/// </summary>
public class IndexShould : TestContext
{
    public IndexShould(ITestOutputHelper outputHelper)
    {
        Services.AddXunitLogger(outputHelper);
				Services.AddScoped<IProductService, FakeProductService>();
    }

    [Fact]
		public void ShowsProductsInitially()
		{
			var cut = RenderComponent<Productenlijst>();
			cut.FindAll(".product-list .product-card").Count.ShouldBe(7);
		}

		[Fact]
		public void ShowsReusableProducts()
		{

    	var cut = RenderComponent<Productenlijst>();

    	cut.Find("button:contains('Herbruikbaar')").Click();

    	cut.FindAll(".product-list .product-card").Count.ShouldBe(3);
		}

		[Fact]
    public void FiltersProductsBasedOnSearch()
    {
        var cut = RenderComponent<Productenlijst>();

        var inputElement = cut.Find("#product-search");
        inputElement.Change("Product 8");  

        cut.Find("button:contains('Zoek')").Click();

        var productCards = cut.FindAll(".product-list .product-card");
        productCards.Count.ShouldBe(1);  

        productCards[0].TextContent.ShouldContain("Product 8");
    }

		[Fact]
		public void SearchReusableProductOnInitialScreen() {
				var cut = RenderComponent<Productenlijst>();

        var inputElement = cut.Find("#product-search");
        inputElement.Change("Product 2");  

        cut.Find("button:contains('Zoek')").Click();

        var productCards = cut.FindAll(".product-list .product-card");
        productCards.Count.ShouldBe(0);  
		}

		[Fact]
		public void SearchOnDescriptionInitialScreen() {
				var cut = RenderComponent<Productenlijst>();

        var inputElement = cut.Find("#product-search");
        inputElement.Change("Product");  

        cut.Find("button:contains('Zoek')").Click();

        var productCards = cut.FindAll(".product-list .product-card");
        productCards.Count.ShouldBe(7);  
		}
}
