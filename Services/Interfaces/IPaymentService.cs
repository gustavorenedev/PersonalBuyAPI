using EcommerceAPI.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.Services.Interfaces;

public interface IPaymentService
{
    Task<string> PaymentCart(int clientId);
}
