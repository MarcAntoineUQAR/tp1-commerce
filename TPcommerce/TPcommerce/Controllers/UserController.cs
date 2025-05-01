using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using TPcommerce.Models.DTO;
using TPcommerce.Repository;

namespace TPcommerce.Controllers
{
    public class UserController : Controller
    {
        private readonly UserRepository _userRepository;
        private readonly HttpClient _httpClient = new HttpClient();

        public UserController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        [HttpGet("user")]
        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetString("JWT");
            if (string.IsNullOrEmpty(token))
            {
                TempData["message"] = "Vous devez être connecté.";
                return RedirectToAction("Index", "Login");
            }

            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);
            var userId = jwt.Claims.FirstOrDefault(c => c.Type == "id")?.Value;

            if (userId == null)
            {
                TempData["message"] = "Jeton invalide.";
                return RedirectToAction("Index", "Login");
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync($"http://localhost:5001/Users/{userId}");

            if (!response.IsSuccessStatusCode)
                return View("Error");

            var user = await response.Content.ReadFromJsonAsync<User>();
            return View("InfoUser", user);
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

