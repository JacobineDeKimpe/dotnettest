using Rise.Shared.Products;
using System.Net.Http.Json;

namespace Rise.Client.Products;

public class ProductService : IProductService
{
    private readonly HttpClient httpClient;

    public ProductService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<IEnumerable<ProductDto>> GetProductsAsync()
    {
        var products = await httpClient.GetFromJsonAsync<IEnumerable<ProductDto>>("product");
        return products!;
    }

    public async Task<ProductDto?> GetProductById(int id)
    {

        var product = await httpClient.GetFromJsonAsync<ProductDto>($"product/{id}");
        return product;
    }

    public async Task<IEnumerable<ProductDto>> GetNotReusableProductsAsync()
    {
        var products = await httpClient.GetFromJsonAsync<IEnumerable<ProductDto>>("product/nonreusable");
        return products!;
    }

    public async Task<IEnumerable<ProductDto>> GetReusableProductsAsync()
    {
        var products = await httpClient.GetFromJsonAsync<IEnumerable<ProductDto>>("product/reusable");
        return products!;
    }
}
