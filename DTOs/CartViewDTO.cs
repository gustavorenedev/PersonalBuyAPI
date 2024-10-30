namespace EcommerceAPI.DTOs;

public class CartViewDTO
{
    public int CartId { get; set; }
    public int ClientId { get; set; }
    public List<CartItemViewDTO> Items { get; set; } = new List<CartItemViewDTO>();

    public class CartItemViewDTO
    {
        public ProductDTO Product { get; set; }
        public int Quantity { get; set; }
    }
}