using TPcommerce.Models;

namespace TPcommerce.Repository;

public class UserRepository
{

    public string Login(LoginViewModel creditentials)
    {
        TpcommerceContext context = new TpcommerceContext();
        var user = context.Users.SingleOrDefault(u => u.Username == creditentials.Username);
        if (user == null)
        {
            return "User not found";
        }

        if (user.Password != creditentials.Password)
        {
            return "Bad Password";

        }
        
        return "Logged in";
    }
    
    public void AddUser(User user)
    {
        TpcommerceContext context = new TpcommerceContext();
        context.Users.Add(user);
        context.SaveChanges();
    }

    public User ShowUserDetails(int userId)
    {
        TpcommerceContext context = new TpcommerceContext();
        User user = context.Users.FirstOrDefault(u => u.Id == userId)!;
        return user;
    }
}