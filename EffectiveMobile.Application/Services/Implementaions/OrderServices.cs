using EffectiveMobile.Application.Repositories.Interfaces;
using EffectiveMobile.Application.Services.Interfaces;
using EffectiveMobile.Domain.Models;
using Microsoft.Extensions.Logging;

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
        var query = _asyncRepository.GetQueryBuilder("Order")
            .When(_cityDistrict.Length > 0, q => q.WhereLike("CityDistrict", _cityDistrict))
            .When(_firstDeliveryDateTime is not null, q => q.WhereRaw("DATEDIFF(HOUR, DeliveryTime, @FirstDeliveryTime) <= 0.5"))
            .Select("OrderNumber", "CityDistrict", "OrderDate", "FloatWeight");

        _logger.LogInformation("SQL: " + query.ToString());
        var result = await _asyncRepository.GetListAsync<OrderModel>(query);
        WriteToFile(result);
    }

    private void WriteToFile(IEnumerable<OrderModel> orders)
    {
        string path = Directory.GetParent(Environment.CurrentDirectory).FullName;
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
