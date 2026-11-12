using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Application.DTOs;

public class ProductDto
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    [Range(0.01, double.MaxValue)]
    public decimal Price { get; set; }

    public int StockQuantity { get; set; }
}