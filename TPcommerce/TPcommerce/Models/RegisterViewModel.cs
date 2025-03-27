namespace TPcommerce.Models;

public class RegisterViewModel
{
    public int Id { get; set; }
    
    public string Username { get; set; }
    
    public string Password { get; set; }
    
    public string ConfirmPassword { get; set; }
    
    public string Role { get; set; }
}