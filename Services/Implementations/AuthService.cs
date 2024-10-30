using EcommerceAPI.DTOs;
using EcommerceAPI.Repositories.Interfaces;
using EcommerceAPI.Services.Interfaces;

namespace EcommerceAPI.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IClientService _clientService;
        private readonly IClientRepository _clientRepository;

        public AuthService(IClientService clientService, IClientRepository clientRepository)
        {
            _clientService = clientService;
            _clientRepository = clientRepository;
        }

        public async Task<string> LoginAsync(AuthDTO authDto)
        {
            var user = await _clientRepository.GetClientByEmailAsync(authDto.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(authDto.Password, user.Password))
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            return "Login successful";
        }

        public async Task<ClientDTO> RegisterAsync(ClientDTO clientDto)
        {
            var existingUser = await _clientRepository.GetClientByEmailAsync(clientDto.Email);

            if (existingUser != null)
            {
                throw new InvalidOperationException("Email already in use.");
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(clientDto.Password);

            var clientWithHashedPassword = new ClientDTO
            {
                Name = clientDto.Name,
                Email = clientDto.Email,
                Address = clientDto.Address,
                Password = hashedPassword
            };

            return await _clientService.CreateClientAsync(clientWithHashedPassword);
        }
    }
}
