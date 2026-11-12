using InventoryManagement.Application.Interfaces;
using InventoryManagement.Domain.Interfaces;

namespace InventoryManagement.Application.Services;

public class StockService : IStockService
{
    private readonly IProductRepository _productRepository;

    public StockService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<bool> AddStockAsync(int productId, int quantity)
    {
        var product = await _productRepository.GetByIdAsync(productId);

        if (product == null || quantity <= 0)
        {
            return false;
        }

        product.StockQuantity += quantity;

        await _productRepository.UpdateAsync(product);

        return true;
    }

    public async Task<bool> ReduceStockAsync(int productId, int quantity)
    {
        var product = await _productRepository.GetByIdAsync(productId);

        if (product == null || quantity <= 0)
        {
            return false;
        }

        if (product.StockQuantity < quantity)
        {
            return false;
        }

        product.StockQuantity -= quantity;

        await _productRepository.UpdateAsync(product);

        return true;
    }

    public async Task<int?> GetStockAsync(int productId)
    {
        var product = await _productRepository.GetByIdAsync(productId);

        if (product == null)
        {
            return null;
        }

        return product.StockQuantity;
    }
}