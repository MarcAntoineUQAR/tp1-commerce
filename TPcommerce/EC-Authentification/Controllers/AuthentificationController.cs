using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using EC_Authentification.Models;

namespace EC_Authentification.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthentificationController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly EcAuthentificationContext _context;
    

    public AuthentificationController(IConfiguration configuration, EcAuthentificationContext context)
    {
        _configuration = configuration;
        _context = context;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest dto)
    {
        var response = await new HttpClient().PostAsJsonAsync("http://localhost:5001/Users/validate", dto);

        if (!response.IsSuccessStatusCode)
            return Unauthorized("Nom d'utilisateur ou mot de passe incorrect.");

        var userId = await response.Content.ReadAsStringAsync();

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("id", userId),
                new Claim(ClaimTypes.Name, dto.Username),
                new Claim(ClaimTypes.Role, "Seller")
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"],
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwt = tokenHandler.WriteToken(token);

        return Ok(new { token = jwt });
    }

}