using AutoMapper;
using EcommerceAPI.DTOs;
using EcommerceAPI.Model;
using EcommerceAPI.Repositories.Interfaces;
using EcommerceAPI.Services.Interfaces;

namespace EcommerceAPI.Services.Implementations;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;
    private readonly IMapper _mapper;

    public ClientService(IClientRepository clientRepository, IMapper mapper)
    {
        _clientRepository = clientRepository;
        _mapper = mapper;
    }

    public async Task<ClientDTO> CreateClientAsync(ClientDTO clientDto)
    {
        var client = _mapper.Map<Client>(clientDto);
        var createdClient = await _clientRepository.CreateClientAsync(client);
        return _mapper.Map<ClientDTO>(createdClient);
    }

    public async Task DeleteClientAsync(int clientId)
    {
        await _clientRepository.DeleteClientAsync(clientId);
    }

    public async Task<IEnumerable<ClientDTO>> GetAllClientsAsync()
    {
        var clients = await _clientRepository.GetClientsAsync();
        return _mapper.Map<IEnumerable<ClientDTO>>(clients);
    }

    public async Task<ClientDTO> GetClientByIdAsync(int clientId)
    {
        var client = await _clientRepository.GetClientByIdAsync(clientId);
        return _mapper.Map<ClientDTO>(client);
    }

    public async Task<ClientDTO> UpdateClientAsync(int clientId, ClientDTO clientDto)
    {
        var client = _mapper.Map<Client>(clientDto);
        var updatedClient = await _clientRepository.UpdateClientAsync(clientId, client);
        return _mapper.Map<ClientDTO>(updatedClient);
    }
}