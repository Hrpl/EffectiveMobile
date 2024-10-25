using EffectiveMobile.Application.Repositories.Interfaces;
using EffectiveMobile.Application.Services.Interfaces;
using EffectiveMobile.Domain.Entities;
using EffectiveMobile.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SqlKata;
using SqlKata.Execution;
using System.Data;
using System.Numerics;

namespace EffectiveMobile.Application.Services.Implementaions;

public class OrderServices : IOrderServices
{
    private readonly IAsyncRepository _asyncRepository;
    private readonly IWriterService _writerService;
    private readonly ILogger<OrderServices> _logger;

    public OrderServices(IAsyncRepository asyncRepository, ILogger<OrderServices> logger, IWriterService writerService)
    {
        _asyncRepository = asyncRepository;
        _logger = logger;
        _writerService = writerService;
    }
    public async Task<List<OrderModel>> GetOrders(string _cityDistrict, string _firstDeliveryDateTime)
    {
        DateTime firstDeliveryDateTime = DateTime.Now;
        
        try
        {
            firstDeliveryDateTime = DateTime.Parse(_firstDeliveryDateTime);
        }
        catch (Exception ex)
        {
            throw new FormatException("Неверный формат данных: " + ex.Message);
        }

        var query = _asyncRepository.GetQueryBuilder("Orders")
            .Where("CityDistrict", _cityDistrict)
            .WhereBetween<DateTime>("OrderDate", firstDeliveryDateTime, firstDeliveryDateTime.AddMinutes(30))
            .Select("*");
        
        var result = await _asyncRepository.GetListAsync<OrderModel>(query);
        _writerService.WriteToFile(result.ToList());
        return result.ToList(); 
    }
}
