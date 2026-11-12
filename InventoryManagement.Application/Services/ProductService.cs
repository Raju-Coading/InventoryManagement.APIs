using InventoryManagement.Application.DTOs;
using InventoryManagement.Application.Interfaces;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Interfaces;

namespace InventoryManagement.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductDto> CreateAsync(ProductDto dto)
    {
        var product = new Product
        {
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            StockQuantity = 0
        };

        var createdProduct = await _productRepository.AddAsync(product);

        return new ProductDto
        {
            Name = createdProduct.Name,
            Description = createdProduct.Description,
            Price = createdProduct.Price
        };
    }

    public async Task<IEnumerable<ProductDto>> GetAllAsync()
    {
        var products = await _productRepository.GetAllAsync();

        return products.Select(product => new ProductDto
        {
            Name = product.Name,
            Description = product.Description,
            Price = product.Price
        });
    }

    public async Task<ProductDto?> GetByIdAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product == null)
        {
            return null;
        }

        return new ProductDto
        {
            Name = product.Name,
            Description = product.Description,
            Price = product.Price
        };
    }

    public async Task<bool> UpdateAsync(int id, ProductDto dto)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product == null)
        {
            return false;
        }

        product.Name = dto.Name;
        product.Description = dto.Description;
        product.Price = dto.Price;

        await _productRepository.UpdateAsync(product);

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product == null)
        {
            return false;
        }

        await _productRepository.DeleteAsync(product);

        return true;
    }
}