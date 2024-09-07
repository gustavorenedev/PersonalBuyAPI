using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceAPI.Model;

[Table("TB_ItemCart_PX")]
public class ItemCart
{
    [Key]
    public int ItemCartId { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public int Quantity { get; set; }
    public int CartId { get; set; }

    // Relacionamento com o carrinho
    [ForeignKey("CartId")]
    public Cart Cart { get; set; }
}
