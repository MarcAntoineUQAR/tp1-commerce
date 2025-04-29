using Microsoft.AspNetCore.Mvc;
using TPcommerce.Models;
using TPcommerce.Repository;
using Microsoft.AspNetCore.Http;
using TPcommerce.Models.DTO;

namespace TPcommerce.Controllers
{
    public class UserController : Controller
    {
        private readonly UserRepository _userRepository;

        public UserController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        [HttpGet("user")]
        public IActionResult Index()
        {
            // var userId = HttpContext.Session.GetInt32("UserId");
            // if (userId == null)
            // {
            //     TempData["message"] = "Êtes-vous bien connecté?";
            //     return RedirectToAction("Login", "Login");
            // }
            // var user = _userRepository.ShowUserDetails(userId.Value);
            //
            // return View("../user/InfoUser", user);
            return new EmptyResult();
        }

        [HttpGet("user/update")]
        public IActionResult GetUpdateUser(User user)
        {
            // return View("../user/UpdateUser", user);
            return new EmptyResult();
        }

        [HttpPost("user/update")]
        public IActionResult UpdateUser([FromForm] User user)
        {
            // var result = _userRepository.UpdateUser(user.Id, user);
            // if (result.Success)
            // {
            //     TempData["message"] = result.Message;
            //     return RedirectToAction("Index");
            // }
            // else
            // {
            //     TempData["message"] = result.Message;
            //     return RedirectToAction("Index");
            // }
            return new EmptyResult();

        }
    }
}

