using EcommerceAPI.DTOs;
using EcommerceAPI.Services.Interfaces;

namespace EcommerceAPI.Services.Implementations;

public class AuthService : IAuthService
{
    public Task<string> AuthenticateAsync(AuthDTO authDto)
    {
        throw new NotImplementedException();
    }

    public Task RegisterAsync(ClientDTO clientDto)
    {
        throw new NotImplementedException();
    }
}