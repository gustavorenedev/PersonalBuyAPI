namespace EcommerceAPI.DTOs;

public class CartDTO
{
    public int CartId { get; set; }
    public int ClientId { get; set; }
    public List<CartItemDTO> Items { get; set; } = new List<CartItemDTO>();

    public class CartItemDTO
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
