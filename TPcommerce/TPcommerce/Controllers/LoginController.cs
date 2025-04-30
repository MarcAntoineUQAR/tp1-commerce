using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TPcommerce.Models;
using TPcommerce.Repository;

namespace TPcommerce.Controllers;

public class LoginController : Controller
{
    public LoginController()
    {
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View("../User/Login");
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginPost([FromForm] LoginViewModel user)
    {
        using var httpClient = new HttpClient();

        var response = await httpClient.PostAsJsonAsync("http://localhost:5002/Authentification/login", new
        {
            Username = user.Username,
            Password = user.Password
        });

        if (!response.IsSuccessStatusCode)
        {
            ViewBag.Error = "Nom d'utilisateur ou mot de passe incorrect.";
            return View("../User/Login");
        }

        var result = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
        var token = result["token"];

        HttpContext.Session.SetString("JWT", token);

        return RedirectToAction("Index", "Home");
    }




    [HttpGet("logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Remove("UserToken");
        TempData["message"] = "Logout successful";
        return RedirectToAction("Login", "Login");
    }

    [HttpGet("register")]
    public IActionResult GetRegister()
    {
        return View("../User/Register");
    }
}