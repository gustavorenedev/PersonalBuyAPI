using EcommerceAPI.DTOs;

namespace EcommerceAPI.Services.Interfaces;

public interface IClientService
{
    Task<ClientDTO> GetClientByIdAsync(int clientId);
    Task<IEnumerable<ClientDTO>> GetAllClientsAsync();
    Task<ClientDTO> CreateClientAsync(ClientDTO clientDto);
    Task<ClientDTO> UpdateClientAsync(int clientId, ClientDTO clientDto);
    Task DeleteClientAsync(int clientId);
}
