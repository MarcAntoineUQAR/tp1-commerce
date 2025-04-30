using System.Net.Http.Headers;
using System.Text.Json;
using TPcommerce.Models;
using TPcommerce.Models.DTO;

namespace TPcommerce.Repository;

public class UserRepository
{
    private readonly HttpClient _httpClient;
    private JsonSerializerOptions _jsonSerializerOptions;

    public UserRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _jsonSerializerOptions = new JsonSerializerOptions();
    }

    public GenericResponse<User> AddUser(RegisterViewModel creditentials)
    {
        // if (creditentials.Password != creditentials.ConfirmPassword)
        //     return new GenericResponse<User>("Mot de passe ne concorde pas", false);
        //
        // TpcommerceContext context = new TpcommerceContext();
        //
        // var cart = new ShoppingCart();
        // var user = new User
        // {
        //     Username = creditentials.Username,
        //     Password = creditentials.Password,
        //     Role = creditentials.Role,
        //     ShoppingCart = cart
        // };
        // cart.Owner = user;
        //
        // try
        // {
        //     context.Users.Add(user);
        //     context.SaveChanges();
        // }
        // catch (Exception e)
        // {
        //     return new GenericResponse<User>("Erreur inattendue: " + e, false);
        // }
        //
        // return new GenericResponse<User>("Utilisateur ajouté", true);
        return null;
    }


    public async Task<User> ShowUserDetails(int userId, string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.GetAsync($"http://localhost:5001/Users/{userId}");

        if (!response.IsSuccessStatusCode)
            throw new Exception("Impossible de récupérer l'utilisateur.");

        var user = await response.Content.ReadFromJsonAsync<User>();
        return user!;
    }


    public GenericResponse<User> UpdateUser(int id, User user)
    {
        // using var context = new TpcommerceContext();
        //
        // var oldUser = context.Users.FirstOrDefault(u => u.Id == id);
        // if (oldUser == null)
        // {
        //     return new GenericResponse<User>("Utilisateur introuvable", false);
        // }
        //
        // oldUser.Username = user.Username;
        // oldUser.Password = user.Password;
        // oldUser.Role = user.Role;
        //
        // try
        // {
        //     context.Users.Update(oldUser);
        //     context.SaveChanges();
        // }
        // catch (Exception e)
        // {
        //     return new GenericResponse<User>("Erreur inattendue: " + e.Message, false);
        // }
        //
        // return new GenericResponse<User>("Utilisateur modifié avec succès!", true);
        return null;
    }
}