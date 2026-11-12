namespace InventoryManagement.Application.Interfaces;

public interface IStockService
{
    Task<bool> AddStockAsync(int productId, int quantity);

    Task<bool> ReduceStockAsync(int productId, int quantity);

    Task<int?> GetStockAsync(int productId);
}