using EcommerceAPI.DbContext;
using EcommerceAPI.Model;
using EcommerceAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EcommerceAPI.Repositories.Implementations;

public class ClientRepository : IClientRepository
{
    private readonly ApplicationContext _context;

    public ClientRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<Client> CreateClientAsync(Client client)
    {
        await _context.Clients.AddAsync(client);
        await _context.SaveChangesAsync();
        return client;
    }

    public async Task DeleteClientAsync(int clientId)
    {
        var client = await _context.Clients.FindAsync(clientId);
        if (client != null)
        {
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<Client> GetClientByEmailAsync(string email)
    {
        return await _context.Clients
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Email == email);
    }

    public async Task<Client> GetClientByIdAsync(int clientId)
    {
        return await _context.Clients
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.ClientId == clientId);
    }


    public async Task<List<Client>> GetClientsAsync()
    {
        return await _context.Clients.ToListAsync();
    }

    public async Task<Client> UpdateClientAsync(int clientId, Client client)
    {
        var existingClient = await _context.Clients.FindAsync(clientId);
        if (existingClient != null)
        {
            existingClient.Name = client.Name;
            existingClient.Email = client.Email;
            existingClient.Password = client.Password;
            existingClient.Address = client.Address;

            _context.Clients.Update(existingClient);
            await _context.SaveChangesAsync();
            return existingClient;
        }

        return null;
    }
}
