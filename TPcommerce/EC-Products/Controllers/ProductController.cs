using System.Diagnostics;
using EC_Products.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EC_Products.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController(ECProductsContext context) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        try
        {
            var products = await context.Product.ToListAsync();
            return Ok(products);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }

    // Get product by ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(int id)
    {
        try
        {
            var product = await context.Product.FindAsync(id);
            
            if (product == null)
            {
                return NotFound($"Product with ID {id} not found.");
            }

            return Ok(product);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }

    // Add a new product
    [HttpPost]
    public async Task<IActionResult> AddProduct(Product product)
    {
        try
        {
            if (product == null)
            {
                return BadRequest("Product is null.");
            }

            context.Product.Add(product);
            await context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }
        catch (DbUpdateException dbEx)
        {
            Debug.WriteLine(dbEx.Message);
            return StatusCode(500, "Database update error: " + dbEx.Message);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        try
        {
            var product = await context.Product.FindAsync(id);
            
            if (product == null)
            {
                return NotFound($"Product with ID {id} not found.");
            }

            context.Product.Remove(product);
            await context.SaveChangesAsync();

            return Ok("Product deleted");
        }
        catch (DbUpdateException dbEx)
        {
            Debug.WriteLine(dbEx.Message);
            return StatusCode(500, "Database update error: " + dbEx.Message);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }
}
