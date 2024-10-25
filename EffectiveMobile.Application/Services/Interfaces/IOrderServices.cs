using EffectiveMobile.Domain.Models;
using System.Threading.Tasks;

namespace EffectiveMobile.Application.Services.Interfaces;

public interface IOrderServices
{
    public Task<List<OrderModel>> GetOrders(string _cityDistrict, string _firstDeliveryDateTime);
}
