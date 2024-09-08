using EcommerceAPI.Model;

namespace EcommerceAPI.Repositories.Interfaces;

public interface IClientRepository
{
    Task<List<Client>> GetClientsAsync();
    Task<Client> CreateClientAsync(Client client);
    Task<Client> UpdateClientAsync(int clientId, Client client);
    Task DeleteClientAsync(int clientId);
    Task<Client> GetClientByIdAsync(int clientId);
    Task<Client> GetClientByEmailAsync(string email);
}
