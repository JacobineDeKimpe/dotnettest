﻿using Microsoft.EntityFrameworkCore;
using Rise.Persistence;
using Rise.Shared.Products;

namespace Rise.Services.Products;

public class ProductService : IProductService
{
    private readonly ApplicationDbContext dbContext;

    public ProductService(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<IEnumerable<ProductDto>> GetProductsAsync()
    {
        IQueryable<ProductDto> query = dbContext.Products.Select(x => new ProductDto
            {
                Id = x.Id,
                Name = x.Name,
                Location = x.Location,
                Description = x.Description,
                Reusable = x.Reusable,
                Quantity = x.Quantity,
                Barcode = x.Barcode
            });

        var products = await query.ToListAsync();

        return products;
    }

    private async Task<IEnumerable<ProductDto>> GetProductsByReusabilityAsync(bool isReusable)
    {
        IQueryable<ProductDto> query = dbContext.Products
            .Where(x => x.Reusable == isReusable)
            .Select(x => new ProductDto
            {
                Id = x.Id,
                Name = x.Name,
                Location = x.Location,
                Description = x.Description,
                Reusable = x.Reusable,
                Quantity = x.Quantity,
                Barcode = x.Barcode
            });

        var products = await query.ToListAsync();

        return products;
    }

    public async Task<ProductDto?> GetProductById(int id)
    {
        var product = await dbContext.Products
            .Where(x => x.Id == id)
            .Select(x => new ProductDto
            {
                Id = x.Id,
                Name = x.Name,
                Location = x.Location,
                Description = x.Description,
                Reusable = x.Reusable,
                Quantity = x.Quantity,
                Barcode = x.Barcode
            })
            .FirstOrDefaultAsync();

        return product;
    }

    public Task<IEnumerable<ProductDto>> GetNotReusableProductsAsync()
    {
        return GetProductsByReusabilityAsync(false);
    }

    
    public Task<IEnumerable<ProductDto>> GetReusableProductsAsync()
    {
        return GetProductsByReusabilityAsync(true);
    }
}