using InventoryManagement.Application.Services;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Interfaces;
using Moq;

namespace InventoryManagement.Tests;

public class StockServiceTests
{
    [Fact]
    public async Task AddStockAsync_ShouldIncreaseStock()
    {
        // Arrange
        var product = new Product
        {
            Id = 1,
            Name = "Laptop",
            StockQuantity = 10
        };

        var repository = new Mock<IProductRepository>();

        repository
            .Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync(product);

        var service = new StockService(repository.Object);

        // Act
        var result = await service.AddStockAsync(1, 5);

        // Assert
        Assert.True(result);
        Assert.Equal(15, product.StockQuantity);
    }
    [Fact]
    public async Task ReduceStockAsync_ShouldNotAllowNegativeStock()
    {
        // Arrange
        var product = new Product
        {
            Id = 1,
            Name = "Laptop",
            StockQuantity = 10
        };

        var repository = new Mock<IProductRepository>();

        repository
            .Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync(product);

        var service = new StockService(repository.Object);

        // Act
        var result = await service.ReduceStockAsync(1, 15);

        // Assert
        Assert.False(result);
        Assert.Equal(10, product.StockQuantity);
    }


    [Fact]
    public async Task AddStockAsync_ShouldReturnFalse_WhenProductNotFound()
    {
        // Arrange
        var repository = new Mock<IProductRepository>();

        repository
            .Setup(r => r.GetByIdAsync(99))
            .ReturnsAsync((Product?)null);

        var service = new StockService(repository.Object);

        // Act
        var result = await service.AddStockAsync(99, 5);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task AddStockAsync_ShouldReturnFalse_WhenQuantityIsInvalid()
    {
        // Arrange
        var product = new Product
        {
            Id = 1,
            Name = "Laptop",
            StockQuantity = 10
        };

        var repository = new Mock<IProductRepository>();

        repository
            .Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync(product);

        var service = new StockService(repository.Object);

        // Act
        var result = await service.AddStockAsync(1, 0);

        // Assert
        Assert.False(result);
        Assert.Equal(10, product.StockQuantity);
    }

    [Fact]
    public async Task GetStockAsync_ShouldReturnCurrentStock()
    {
        // Arrange
        var product = new Product
        {
            Id = 1,
            Name = "Laptop",
            StockQuantity = 25
        };

        var repository = new Mock<IProductRepository>();

        repository
            .Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync(product);

        var service = new StockService(repository.Object);

        // Act
        var result = await service.GetStockAsync(1);

        // Assert
        Assert.Equal(25, result);
    }
    
}