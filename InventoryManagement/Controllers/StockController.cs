using InventoryManagement.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class StockController : ControllerBase
{
    private readonly IStockService _stockService;

    public StockController(IStockService stockService)
    {
        _stockService = stockService;
    }

    [HttpPost("add/{productId}")]
    public async Task<IActionResult> AddStock(
        int productId,
        int quantity)
    {
        var result = await _stockService.AddStockAsync(
            productId,
            quantity);

        if (!result)
        {
            return BadRequest("Invalid product or quantity.");
        }

        return Ok("Stock added successfully.");
    }

    [HttpPost("reduce/{productId}")]
    public async Task<IActionResult> ReduceStock(
        int productId,
        int quantity)
    {
        var result = await _stockService.ReduceStockAsync(
            productId,
            quantity);

        if (!result)
        {
            return BadRequest(
                "Invalid product, quantity, or insufficient stock.");
        }

        return Ok("Stock reduced successfully.");
    }

    [HttpGet("{productId}")]
    public async Task<IActionResult> GetStock(int productId)
    {
        var stock = await _stockService.GetStockAsync(productId);

        if (stock == null)
        {
            return NotFound("Product not found.");
        }

        return Ok(stock);
    }
}