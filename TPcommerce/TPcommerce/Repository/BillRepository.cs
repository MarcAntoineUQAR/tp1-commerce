using Microsoft.EntityFrameworkCore;
using TPcommerce.Models;

namespace TPcommerce.Repository;

public class BillRepository
{
    private readonly TpcommerceContext _context;

    public BillRepository()
    {
        _context = new TpcommerceContext();
    }

    public List<Bill> GetBillsByUserId(int userId)
    {
        return _context.Bills
            .Where(b => b.OwnerId == userId.ToString())
            .Include(b => b.Products)
                .ThenInclude(p => p.Product)
            .Include(b => b.PaymentInfos)
            .OrderByDescending(b => b.Id)
            .ToList();
    }
}
