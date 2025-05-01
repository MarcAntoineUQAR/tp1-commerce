namespace EC_Orders.Models;

public class Order
{
    public int Id { get; set; }
    
    public int OwnerId { get; set; }

    public List<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
    
    public int TotalPrice { get; set; }
    
    public int PaymentInfosId { get; set; }
}