using EC_Carts.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EC_Carts.Controllers;

[ApiController]
[Route("[controller]")]
public class CartController(ECCartsContext context) : ControllerBase
{
    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetCartByUserId(int userId)
    {
        var cart = await context.ShoppingCarts
            .Include(c => c.Items)
            .FirstOrDefaultAsync(s => s.OwnerId == userId);

        if (cart == null)
            return NotFound();

        var dto = new ShoppingCart()
        {
            Id = cart.Id, 
            OwnerId = cart.OwnerId,
            Items = cart.Items.Select(i => new ShoppingCartItem
            {
                Id = i.Id,
                ProductId = i.ProductId,
                Quantity = i.Quantity
            }).ToList()
        };

        return Ok(dto);
    }



    [HttpPost("create")]
    public async Task<IActionResult> AddCart([FromQuery] int ownerId)
    {
        try
        {
            var cart = new ShoppingCart { OwnerId = ownerId };
            await context.ShoppingCarts.AddAsync(cart);
            await context.SaveChangesAsync();
            return Ok(cart);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPut("{cartId}/add-item")]
    public async Task<IActionResult> AddShoppingCartItemToShoppingCart(int cartId, [FromQuery] int productId, [FromQuery] int quantity)
    {
        try
        {
            var cart = await context.ShoppingCarts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.Id == cartId);

            if (cart == null)
                return NotFound("Cart not found");

            var item = new ShoppingCartItem
            {
                ProductId = productId,
                Quantity = quantity,
                ShoppingCartId = cartId
            };

            await context.ShoppingCartItems.AddAsync(item);
            await context.SaveChangesAsync();

            return Ok("Product added to cart");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpDelete("{cartId}/remove-item")]
    public async Task<IActionResult> RemoveItemFromShoppingCart(int cartId, [FromQuery] int productId)
    {
        try
        {
            var item = await context.ShoppingCartItems
                .FirstOrDefaultAsync(i => i.ShoppingCartId == cartId && i.ProductId == productId);

            if (item == null)
                return NotFound("Item not found");

            context.ShoppingCartItems.Remove(item);
            await context.SaveChangesAsync();

            return Ok("Product removed from cart");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpDelete("{cartId}/clear")]
    public async Task<IActionResult> ClearShoppingCart(int cartId)
    {
        try
        {
            var items = await context.ShoppingCartItems
                .Where(i => i.ShoppingCartId == cartId)
                .ToListAsync();

            if (!items.Any())
                return NotFound("Cart already empty or not found");

            context.ShoppingCartItems.RemoveRange(items);
            await context.SaveChangesAsync();

            return Ok("Cart cleared");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
