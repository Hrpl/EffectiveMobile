using EffectiveMobile.Application.Repositories.Interfaces;
using EffectiveMobile.Application.Services.Interfaces;
using EffectiveMobile.Domain.Entities;
using EffectiveMobile.Domain.Models;
using EffectiveMobile.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EffectiveMobile.Application.Services.Implementaions;

public class OrderServices : IOrderServices
{
    private readonly IAsyncRepository _asyncRepository;
    private readonly ILogger<OrderServices> _logger;

    public OrderServices(IAsyncRepository asyncRepository, ILogger<OrderServices> logger) 
    {
        _asyncRepository = asyncRepository;
        _logger = logger;
    }
    public async Task GetOrders(string _cityDistrict, string _firstDeliveryDateTime)
    {
        DateTime firstDeliveryDateTime = DateTime.Now;
        try
        {
            firstDeliveryDateTime = Convert.ToDateTime(_firstDeliveryDateTime);
        }
        catch (Exception ex)
        {
            throw new FormatException("Неверный формат данных: " + ex.Message);
        }

        var query = _asyncRepository.GetQueryBuilder("Orders")
            .When(_cityDistrict.Length > 0, q => q.WhereLike("CityDistrict", _cityDistrict))
            .When(_firstDeliveryDateTime is not null, q => q.WhereRaw($"DATEDIFF(hour, {firstDeliveryDateTime}, {firstDeliveryDateTime.AddMinutes(30)}) <= 0.5"))
            .Select("OrderNumber", "CityDistrict", "OrderDate", "FloatWeight");

        _logger.LogInformation("SQL: " + query.ToString());
        var result = await _asyncRepository.GetListAsync<OrderModel>(query);
        WriteToFile(result.ToList());
    }

    private void WriteToFile(List<OrderModel> orders)
    {
        string path = Environment.CurrentDirectory;
        string filePath = Path.Combine(path, "filtered_orders.txt");
        _logger.LogInformation("Путь к файлу с результатом фильтрации: " + filePath);
        using (StreamWriter writer = new StreamWriter(filePath, true))
        {
            _logger.LogInformation("Начало записи файла.");
            writer.WriteLine($"I{DateTime.Now}: Результаты выборки");
            foreach (var record in orders)
            {
                writer.WriteLine($"Номер заказа: {record.OrderNumber}, район: {record.CityDistrict}, время дсотавки: {record.OrderDate.ToString("yyyy-MM-dd HH:mm:ss")}, вес: {record.FloatWeight}");
            }
            writer.Close();
        }
    }
}
