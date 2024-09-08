namespace EcommerceAPI.DTOs;

public class ProductDTO
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public int Quantity { get; set; }
}
