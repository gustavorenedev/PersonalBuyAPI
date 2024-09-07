using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceAPI.Model;

[Table("TB_Client_PX")]
public class Client
{
    [Key]
    public int ClientId { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string Email { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public ICollection<Cart> Carts { get; set; } = new List<Cart>();
}
