using InventoryManagement.Domain.Entities;

namespace InventoryManagement.Domain.Interfaces;

public interface IProductRepository
{
    Task<Product> AddAsync(Product product);

    Task<IEnumerable<Product>> GetAllAsync();

    Task<Product?> GetByIdAsync(int id);

    Task UpdateAsync(Product product);

    Task DeleteAsync(Product product);
}