using EffectiveMobile.Application.Services.Interfaces;
using EffectiveMobile.Domain.Exceptions;
using EffectiveMobile.Domain.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EffectiveMobile.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrderServices _orderServices;
    private readonly ILogger<OrderController> _logger;

    public OrderController(IOrderServices orderServices, ILogger<OrderController> logger)
    {
        _orderServices = orderServices;
        _logger = logger;
    }

    // GET: api/<OrderController>
    [HttpGet]
    public async Task<List<OrderModel>> Get(string _cityDistrict = "", string _firstDeliveryDateTime = "")
    {
        _logger.LogInformation("Вход в endpoint получения заказов.");
        _logger.LogInformation($"Параметры фильтрации \"{_cityDistrict}\", \"{_firstDeliveryDateTime}\".");
        if (_cityDistrict == "" || _firstDeliveryDateTime == "") throw new NullParametrException("Один или несколько параметров запроса были пусты");
        return await _orderServices.GetOrders(_cityDistrict, _firstDeliveryDateTime);
    }

}
