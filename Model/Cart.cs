using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceAPI.Model;

[Table("TB_Cart_PX")]
public class Cart
{
    [Key]
    public int CartId { get; set; }
    public int ClientId { get; set; }

    [ForeignKey("ClientId")]
    public Client Client { get; set; }
    public List<ItemCart> Items { get; set; } = new();
}
