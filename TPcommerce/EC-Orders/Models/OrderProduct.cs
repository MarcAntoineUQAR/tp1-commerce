namespace EC_Orders.Models
{
    public class OrderProduct
    {
        public int Id { get; set; }
        
        public int OrderId { get; set; }
        public Order Order { get; init; }

        public int ProductId { get; init; }

        public int Quantity { get; init; } 
    }
}