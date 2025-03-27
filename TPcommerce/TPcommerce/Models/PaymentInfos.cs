namespace TPcommerce.Models;

public class PaymentInfos
{
    public int Id { get; set; }
    
    public string FullName { get; set; }
    
    public string CardType { get; set; }
    
    public string CardNumber { get; set; }
    
    public string ExpirationDate { get; set; }
    
    public string CVV { get; set; }
}