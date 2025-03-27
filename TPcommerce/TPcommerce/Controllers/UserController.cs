using Microsoft.AspNetCore.Mvc;
using TPcommerce.Models;
using TPcommerce.Repository;
using Microsoft.AspNetCore.Http;

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
            return View("InfoUser");
        }
    }
}
