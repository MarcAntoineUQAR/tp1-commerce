namespace TPcommerce.Models;

public class Bill
{
    public int Id { get; set; }
    public List<BillItem> Products { get; set; } = new List<BillItem>();
    public int TotalPrice { get; set; }
    public PaymentInfos PaymentInfos { get; set; }
    public string OwnerId { get; set; }
}
