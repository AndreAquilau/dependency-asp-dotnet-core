using Dapper;
using DependencyStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using RestSharp;
using DependencyStore.Repositories.Contracts;
using DependencyStore.Services.Contracts;

namespace DependencyStore.Controllers;

public class OrderController : ControllerBase
{

    private readonly ICustomerRepository _customerRepository;
    private readonly IDeliveryFeeService _deliveryFeeService;
    private readonly IPromoCodeRepository _promoCodeRepository;

    public OrderController(
        ICustomerRepository customerRepository, 
        IDeliveryFeeService deliveryFeeService, 
        IPromoCodeRepository promotionCodeRepository)
    {
        _customerRepository = customerRepository;
        _deliveryFeeService = deliveryFeeService;
        _promoCodeRepository = promotionCodeRepository;
    }

    [Route("v1/orders")]
    [HttpPost]
    public async Task<IActionResult> Place(string customerId, string zipCode, string promoCode, int[] products)
    {
        // #1 - Recupera o cliente
        Customer? customer = null;

        customer = await _customerRepository.GetByIdAsync(customerId);
    
        if (customer == null)
        {
            return NotFound();
        }

        // #2 - Calcula o frete


        var deliveryFee = await _deliveryFeeService.GetDeliveryFeeAsync(zipCode);
        var cupon = await _promoCodeRepository.GetPromoCodeAsync(promoCode);
        var discount = cupon?.Value ?? 0;

        var order = new Order(deliveryFee, discount, new List<Product>());

        return Ok($"Pedido {order.Code} gerado com sucesso!");
    }
}