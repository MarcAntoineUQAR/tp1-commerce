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
        // return View("../User/Login");
        return new EmptyResult();
    }

    [HttpPost("login")]
    public IActionResult LoginPost([FromForm] LoginViewModel user)
    {
        // var result = _userRepository.Login(user);
        // TempData["message"] = result.Message;
        // if (result.Success)
        // {
        //     HttpContext.Session.SetInt32("UserId", result.Data.Id);
        //     HttpContext.Session.SetString("Role", result.Data.Role);
        //     return RedirectToAction("Index", "Home");
        // }
        // return RedirectToAction("Login");
        return new EmptyResult();
    }


    [HttpGet("logout")]
    public IActionResult Logout()
    {
        // HttpContext.Session.Remove("UserToken");
        // TempData["message"] = "Logout successful";
        // return RedirectToAction("Login", "Login");
        return new EmptyResult();
    }

    [HttpGet("register")]
    public IActionResult GetRegister()
    {
        // return View("../User/Register");
        return new EmptyResult();
    }

    [HttpPost]
    public IActionResult Register([FromForm] RegisterViewModel user)
    {
        // var message = _userRepository.AddUser(user);
        // TempData["message"] = message.Message;
        // if (!message.Success)
        // {
            // return RedirectToAction("GetRegister", "Login");
        // }
        // return RedirectToAction("Login", "Login");
        return new EmptyResult();
    }
}