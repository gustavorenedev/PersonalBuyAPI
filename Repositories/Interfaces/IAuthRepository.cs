using EcommerceAPI.Model;

namespace EcommerceAPI.Repositories.Interfaces;

public interface IAuthRepository
{
    Task<Client> AuthenticateAsync(string email, string password);
    Task<Client> GetClientByTokenAsync(string token);
}
