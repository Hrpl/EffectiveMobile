﻿using EffectiveMobile.Application.Services.Interfaces;
using EffectiveMobile.Domain.Models;
using Microsoft.Extensions.Logging;

namespace EffectiveMobile.Application.Services.Implementaions;

public class WriterService : IWriterService
{
    private readonly ILogger<WriterService> _logger;   

    public WriterService(ILogger<WriterService> logger)
    {
        _logger = logger;
    }

    public void WriteToFile(List<OrderModel> orders)
    {
        string projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\.."));
        string filePath = Path.Combine(projectRoot, "filtered_orders.txt");
        _logger.LogInformation("Путь к файлу с результатом фильтрации: " + filePath);
        using (StreamWriter writer = new StreamWriter(filePath, false))
        {
            _logger.LogInformation("Начало записи файла.");
            writer.WriteLine($"I{DateTime.Now}: Результаты выборки");
            foreach (var record in orders)
            {
                writer.WriteLine($"Номер заказа: {record.OrderNumber}, район: {record.CityDistrict}, время доставки: {record.OrderDate.ToString("yyyy-MM-dd HH:mm:ss")}, вес: {record.FloatWeight}");
            }
            writer.Close();
            _logger.LogInformation("Конец записи файла.");
        }
    }
}
