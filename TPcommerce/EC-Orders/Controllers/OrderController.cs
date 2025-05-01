using System.Diagnostics;
using EC_Orders.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EC_Orders.Controllers
{
    [Route("[controller]")]
    public class OrderController(ECOrdersContext context) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Order>>> GetOrders()
        {
            var orders = await context.Orders.ToListAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrderById(int id)
        {
            var order = await context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<Order>> GetOrderFromUserId(int userId)
        {
            var order = await context.Orders
                .FirstOrDefaultAsync(o => o.OwnerId == userId);

            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            await context.Orders.AddAsync(order);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, order);
        }

        [HttpDelete]
        public async Task<ActionResult<Order>> DeleteOrder(int id)
        {
            var order = await context.Orders.FindAsync(id);
            context.Orders.Remove(order);
            await context.SaveChangesAsync();
            return Ok("Deleted");
        }
    }
}