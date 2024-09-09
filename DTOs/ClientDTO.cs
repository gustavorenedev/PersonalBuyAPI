namespace EcommerceAPI.DTOs;

public class ClientDTO
{
    public int ClientId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
