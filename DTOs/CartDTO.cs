using System.Text.Json.Serialization;

namespace EcommerceAPI.DTOs;

public class CartDTO
{
    public int CartId { get; set; }
    public int ClientId { get; set; }
    public List<CartItemDTO> Items { get; set; } = new List<CartItemDTO>();

    public class CartItemDTO
    {
        public ProductDTO Product { get; set; }
        [JsonIgnore]
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
