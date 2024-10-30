using EcommerceAPI.Services.Interfaces;
using System.Text;
using System.Text.Json;

namespace EcommerceAPI.Services.Implementations;

public class PaymentService : IPaymentService
{
    private readonly HttpClient _httpClient;
    private readonly ICartService _cartService;

    public PaymentService(ICartService cartService, HttpClient httpClient)
    {
        _cartService = cartService;
        _httpClient = httpClient;
    }

    public async Task<string> PaymentCart(int clientId)
    {
        var cart = await _cartService.GetCartByClientIdAsync(clientId);

        var jsonBody = JsonSerializer.Serialize(cart.Items);
        var httpContent = new StringContent(jsonBody, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("http://localhost:5268/api/Stripe/Payment", httpContent);

        if (response.IsSuccessStatusCode)
        {
            var paymentUrl = await response.Content.ReadAsStringAsync();
            return paymentUrl;
        }

        return null; 
    }
}
