using Microsoft.AspNetCore.Mvc;
using TPcommerce.Models;
using TPcommerce.Repository;

namespace TPcommerce.Controllers;

public class LoginController : Controller
{
    private readonly UserRepository _userRepository;

    public LoginController(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet("Login")]
    public IActionResult Login()
    {
        return View("../User/Login");
    }

    [HttpPost("login")]
    public IActionResult LoginPost([FromForm] LoginViewModel user)
    {
        var result = _userRepository.Login(user);
        if (result == "Logged in")
        {
            HttpContext.Session.SetString("UserSession", user.Username);
            TempData["message"] = "Login Successful";
            return RedirectToAction("Index", "Home");
        }

        if (result == "User not found")
        {
            TempData["message"] = "Utilisateur non trouvé";
            return RedirectToAction("Login", "Login");
        }

        if (result == "Bad Password")
        {
            TempData["message"] = "Mot de passe incorrect";
            return RedirectToAction("Login", "Login");
        }

        TempData["message"] = "Erreur inconnue";
        return RedirectToAction("Login", "Login");
    }

    [HttpGet("logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Remove("UserSession");
        TempData["message"] = "Logout successful";
        return RedirectToAction("Login", "Login");
    }

    [HttpGet("register")]
    public IActionResult GetRegister()
    {
        return View("../User/Register");
    }

    [HttpPost("register")]
    public IActionResult Register()
    {
        return null; // Complète cette méthode selon ton besoin
    }
}