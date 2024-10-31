using Rise.Domain.Products;
using Rise.Domain.Leveranciers;
using Shouldly;

namespace Rise.Domain.Tests.Products;

/// <summary>
/// Example Domain Tests using xUnit and Shouldly
/// https://xunit.net
/// https://docs.shouldly.org
/// </summary>
public class ProductShould
{
    private readonly Leverancier leverancier = new()
    {
        Name = "Medical Supplies Inc.",
        Email = "info@medsupplies.com",
        Address = "123 Health Street"
    };

    [Fact]
    public void BeCreated()
    {
        var product = new Product
        {
            Name = "Surgical Mask",
            Location = "Medical Supply Room",
            Description = "A disposable surgical mask to protect against airborne pathogens.",
            Reusable = false,
            Quantity = 100,
            Barcode = "123456789",
            Leverancier = leverancier  
        };

        product.Name.ShouldBe("Surgical Mask");
        product.Location.ShouldBe("Medical Supply Room");
        product.Description.ShouldBe("A disposable surgical mask to protect against airborne pathogens.");
        product.Reusable.ShouldBe(false);
        product.Leverancier.ShouldBe(leverancier); 
    }

    [Theory]
    [InlineData(null)]
    [InlineData("   ")]
    [InlineData("")]
    public void NotBeCreatedWithAnInvalidName(string? name)
    {
        Action act = () =>
        {
            var product = new Product
            {
                Name = name!, 
                Location = "Medical Supply Room",
                Description = "A disposable surgical mask to protect against airborne pathogens.",
                Reusable = false,
                Quantity = 100,
                Barcode = "123456789",
                Leverancier = leverancier 
            };
        };

        act.ShouldThrow<ArgumentException>();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("   ")]
    [InlineData("")]
    public void NotBeChangedToHaveAnInvalidName(string? name)
    {
        Product p = new()
        {
            Name = "Medical Product", 
            Location = "Valid Location", 
            Description = "A valid description.", 
            Reusable = true,
            Quantity = 5,
            Barcode = "12345678",
            Leverancier = leverancier 
        };

        Action act = () =>
        {
            p.Name = name!;
        };

        act.ShouldThrow<ArgumentException>();
    }

    [Fact]
    public void NotBeCreatedWithNegativeQuantity()
    {
        Action act = () =>
        {
            var product = new Product
            {
                Name = "Surgical Mask",
                Location = "Medical Supply Room",
                Description = "A disposable surgical mask to protect against airborne pathogens.",
                Reusable = false,
                Quantity = -10,
                Barcode = "123456789",
                Leverancier = leverancier  
            };
        };

        act.ShouldThrow<ArgumentException>();
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-5)]
    [InlineData(-100)]
    public void NotAllowNegativeQuantityAfterCreation(int invalidQuantity)
    {
        var product = new Product
        {
            Name = "Surgical Mask",
            Location = "Medical Supply Room",
            Description = "A disposable surgical mask to protect against airborne pathogens.",
            Reusable = false,
            Quantity = 100, 
            Barcode = "123456789",
            Leverancier = leverancier  
        };

        Action act = () =>
        {
            product.Quantity = invalidQuantity;
        };

        act.ShouldThrow<ArgumentException>();
    }


}
