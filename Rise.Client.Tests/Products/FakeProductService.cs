using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rise.Shared.Products;

namespace Rise.Client.Products;

    public class FakeProductService : IProductService
    {
        private readonly List<ProductDto> _products;

        public FakeProductService()
        {
            // 10 producten: 3 zijn herbruikbaar, 7 niet
            _products = Enumerable.Range(1, 10).Select(i => new ProductDto
            {
                Id = i,
                Name = $"Product {i}",
                Location = $"Location {i}",
                Description = $"Description for Product {i}",
                Reusable = i <= 3,
                Quantity = i * 10,
                Barcode = $"1234567890{i}"
            }).ToList();
        }

        public Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
            return Task.FromResult(_products.AsEnumerable());
        }

        public Task<IEnumerable<ProductDto>> GetNotReusableProductsAsync()
        {
            var notReusableProducts = _products.Where(p => !p.Reusable);
            return Task.FromResult(notReusableProducts);
        }

        public Task<IEnumerable<ProductDto>> GetReusableProductsAsync()
        {
            var reusableProducts = _products.Where(p => p.Reusable);
            return Task.FromResult(reusableProducts);
        }

    Task<ProductDto> IProductService.GetProductById(int productId)
    {
        throw new System.NotImplementedException();
    }
}

