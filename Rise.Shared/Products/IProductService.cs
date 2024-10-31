namespace Rise.Shared.Products;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetProductsAsync();
    Task<IEnumerable<ProductDto>> GetNotReusableProductsAsync();
    Task<IEnumerable<ProductDto>> GetReusableProductsAsync();

    Task<ProductDto> GetProductById(int productId);
}