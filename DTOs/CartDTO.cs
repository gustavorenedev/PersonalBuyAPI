namespace EcommerceAPI.DTOs;

public class CartDTO
{
    public int CartId { get; set; }
    public int ClientId { get; set; }
    public List<CartItemDTO> Items { get; set; } = new List<CartItemDTO>();

    public class CartItemDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
