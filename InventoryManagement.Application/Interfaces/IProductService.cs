using InventoryManagement.Application.DTOs;

namespace InventoryManagement.Application.Interfaces;

public interface IProductService
{
    Task<ProductDto> CreateAsync(ProductDto dto);

    Task<IEnumerable<ProductDto>> GetAllAsync();

    Task<ProductDto?> GetByIdAsync(int id);

    Task<bool> UpdateAsync(int id, ProductDto dto);

    Task<bool> DeleteAsync(int id);
}