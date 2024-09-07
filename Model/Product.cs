using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceAPI.Model;

[Table("TB_Product_PX")]
public class Product
{
    [Key]
    public int ProductId { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public decimal Price { get; set; }
    [Required]
    public string Description { get; set; } = string.Empty;
    [Required]
    public int Quantity { get; set; }
}
