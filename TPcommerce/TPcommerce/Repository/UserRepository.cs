using Microsoft.EntityFrameworkCore;
using TPcommerce.Models;
using TPcommerce.Models.DTO;

namespace TPcommerce.Repository;

public class UserRepository
{

    public GenericResponse<User> Login(LoginViewModel creditentials)
    {
        TpcommerceContext context = new TpcommerceContext();
        var user = context.Users.SingleOrDefault(u => u.Username == creditentials.Username);
        if (user == null)
        {
            return new GenericResponse<User>("Utilisateur introuvable", false);
        }

        if (user.Password != creditentials.Password)
        {
            return new GenericResponse<User>("Mot de passe incorrect", false);
        }
        
        return new GenericResponse<User>(user, "Logging fait avec succès", true);
    }
    
    public GenericResponse<User> AddUser(RegisterViewModel creditentials)
    {
        if(creditentials.Password != creditentials.ConfirmPassword)
            return new GenericResponse<User>("Mot de passe de concorde pas", false);
        
        TpcommerceContext context = new TpcommerceContext();
        var user = new User()
        {
            Username = creditentials.Username,
            Password = creditentials.Password,
            Role = creditentials.Role,
        };

        try
        {
            context.Users.Add(user);
            context.SaveChanges();
        }
        catch (Exception e)
        {
            return new GenericResponse<User>("Erreur inattendu: " + e, false);
        }

        return new GenericResponse<User>("Utilisateur ajouté", true);
    }

    public User ShowUserDetails(int userId)
    {
        TpcommerceContext context = new TpcommerceContext();
        User user = context.Users.Include(u => u.ShoppingCart).FirstOrDefault(u => u.Id == userId);
        return user;
    }

    public GenericResponse<User> UpdateUser(int id, User user)
    {
        using var context = new TpcommerceContext();
    
        var oldUser = context.Users.FirstOrDefault(u => u.Id == id);
        if (oldUser == null)
        {
            return new GenericResponse<User>("Utilisateur introuvable", false);
        }

        oldUser.Username = user.Username;
        oldUser.Password = user.Password;
        oldUser.Role = user.Role;

        try
        {
            context.Users.Update(oldUser);
            context.SaveChanges();
        }
        catch (Exception e)
        {
            return new GenericResponse<User>("Erreur inattendue: " + e.Message, false);
        }

        return new GenericResponse<User>("Utilisateur modifié avec succès!", true);
    }

}