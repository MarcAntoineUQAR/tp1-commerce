using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace EC_Users.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController(EcUsersContext context) : ControllerBase
    {
        
        [HttpPost("validate")]
        public IActionResult ValidateUser(LoginRequest dto)
        {
            var user = context.Users.SingleOrDefault(u => u.Username == dto.Username);

            if (user == null || user.Password != dto.Password)
                return Unauthorized();

            return Ok(user.Id);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await context.Users.ToListAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateUser(User user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest("User data is null.");
                }

                context.Users.Add(user);
                await context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetAllUsers), new { id = user.Id }, user);
            }
            catch (DbUpdateException dbEx)
            {
                return StatusCode(500, $"Database error: {dbEx.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                var user = await context.Users.FindAsync(id);
                if (user == null)
                {
                    return NotFound($"User with ID {id} not found.");
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateUser(int id, User user)
        {
            try
            {
                if (user == null || user.Id != id)
                {
                    return BadRequest("User data is invalid or ID mismatch.");
                }

                var existingUser = await context.Users.FindAsync(id);
                if (existingUser == null)
                {
                    return NotFound($"User with ID {id} not found.");
                }

                context.Users.Update(user);
                await context.SaveChangesAsync();
                return Ok("User updated successfully.");
            }
            catch (DbUpdateException dbEx)
            {
                return StatusCode(500, $"Database error: {dbEx.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var user = await context.Users.FindAsync(id);
                if (user == null)
                {
                    return NotFound($"User with ID {id} not found.");
                }

                context.Users.Remove(user);
                await context.SaveChangesAsync();
                return Ok("User deleted successfully.");
            }
            catch (DbUpdateException dbEx)
            {
                return StatusCode(500, $"Database error: {dbEx.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
