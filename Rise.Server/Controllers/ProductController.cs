using Microsoft.AspNetCore.Mvc;
using Rise.Shared.Products;

namespace Rise.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService productService;

    public ProductController(IProductService productService)
    {
        this.productService = productService;
    }

    [HttpGet]
    public async Task<IEnumerable<ProductDto>> Get()
    {
        var products = await productService.GetProductsAsync();
        return products;
    }

    [HttpGet("nonreusable")]
    public async Task<IEnumerable<ProductDto>> GetNonReusableProducts()
    {
        var products = await productService.GetNotReusableProductsAsync();
        return products;
    }

    [HttpGet("reusable")]
    public async Task<IEnumerable<ProductDto>> GetReusableProducts()
    {
        var products = await productService.GetReusableProductsAsync();
        return products;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetProductById(int id)
    {
        var product = await productService.GetProductById(id);
        if (product == null)
            return NotFound();
        return product;
    }


}
