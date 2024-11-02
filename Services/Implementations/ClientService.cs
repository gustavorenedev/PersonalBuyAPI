using AutoMapper;
using EcommerceAPI.DTOs;
using EcommerceAPI.Model;
using EcommerceAPI.Repositories.Interfaces;
using EcommerceAPI.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

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
        if (clientDto == null)
            throw new ArgumentNullException(nameof(clientDto), "Os dados do cliente não podem ser nulos");

        ValidateClientDto(clientDto);

        var existingClient = await _clientRepository.GetClientByEmailAsync(clientDto.Email);
        if (existingClient != null)
            throw new InvalidOperationException("Já existe um cliente com o mesmo email");

        var client = _mapper.Map<Client>(clientDto);
        var createdClient = await _clientRepository.CreateClientAsync(client);
        return _mapper.Map<ClientDTO>(createdClient);
    }

    public async Task DeleteClientAsync(int clientId)
    {
        var existingClient = await _clientRepository.GetClientByIdAsync(clientId);
        if (existingClient == null)
            throw new KeyNotFoundException($"Cliente com ID {clientId} não encontrado");

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
        if (client == null)
            throw new KeyNotFoundException($"Cliente com ID {clientId} não encontrado");

        return _mapper.Map<ClientDTO>(client);
    }

    public async Task<ClientDTO> UpdateClientAsync(int clientId, ClientDTO clientDto)
    {
        if (clientDto == null)
            throw new ArgumentNullException(nameof(clientDto), "Os dados do cliente não podem ser nulos");

        ValidateClientDto(clientDto);

        var existingClient = await _clientRepository.GetClientByIdAsync(clientId);
        if (existingClient == null)
            throw new KeyNotFoundException($"Cliente com ID {clientId} não encontrado");

        var client = _mapper.Map<Client>(clientDto);
        var updatedClient = await _clientRepository.UpdateClientAsync(clientId, client);
        return _mapper.Map<ClientDTO>(updatedClient);
    }

    private void ValidateClientDto(ClientDTO clientDto)
    {
        // Valida se o nome do cliente foi fornecido
        if (string.IsNullOrWhiteSpace(clientDto.Name))
            throw new ValidationException("O nome é obrigatório");

        // Valida se o email é válido
        if (string.IsNullOrWhiteSpace(clientDto.Email) || !new EmailAddressAttribute().IsValid(clientDto.Email))
            throw new ValidationException("É necessário fornecer um email válido");

        // Valida se a senha foi fornecida
        if (string.IsNullOrWhiteSpace(clientDto.Password))
            throw new ValidationException("A senha é obrigatória");
    }
}