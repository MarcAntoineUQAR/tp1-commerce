using Microsoft.AspNetCore.Mvc;
using TPcommerce.Models;
using TPcommerce.Repository;
using System.Collections.Generic;

namespace TPcommerce.Controllers
{
    public class TransactionHistoryController : Controller
    {
        public IActionResult Index()
        {
            string userId = User.Identity.Name;

            HistoryRepository repo = new HistoryRepository();
            List<Bill> bills = repo.GetBillsByUser(userId);

            return View(bills);
        }
    }
}
