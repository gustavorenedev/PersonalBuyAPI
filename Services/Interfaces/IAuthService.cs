using EcommerceAPI.DTOs;

namespace EcommerceAPI.Services.Interfaces;

public interface IAuthService
{
    Task<string> LoginAsync(AuthDTO authDto);
    Task<ClientDTO> RegisterAsync(ClientDTO clientDto);
}
