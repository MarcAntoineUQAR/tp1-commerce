using Microsoft.AspNetCore.Mvc;
using TPcommerce.Repository;

namespace TPcommerce.Controllers;

public class TransactionHistoryController : Controller
{
    private readonly UserRepository _userRepository;
    private readonly BillRepository _billRepository;

    public TransactionHistoryController(UserRepository userRepository, BillRepository billRepository)
    {
        _userRepository = userRepository;
        _billRepository = billRepository;
    }

    [HttpGet]
    public IActionResult Index()
    {
        // var userId = HttpContext.Session.GetInt32("UserId");
        // if (userId == null)
        // {
        //     TempData["message"] = "Veuillez vous connecter.";
        //     return RedirectToAction("Login", "User");
        // }
        //
        // var user = _userRepository.ShowUserDetails(userId.Value);
        // var bills = _billRepository.GetBillsByUserId(user.Id);
        //
        // return View("~/Views/TransactionHistory.cshtml", bills);
        return new EmptyResult();
    }
}
