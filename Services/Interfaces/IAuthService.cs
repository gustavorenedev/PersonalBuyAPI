using EcommerceAPI.DTOs;

namespace EcommerceAPI.Services.Interfaces;

public interface IAuthService
{
    Task<string> AuthenticateAsync(AuthDTO authDto);
    Task RegisterAsync(ClientDTO clientDto);
}
