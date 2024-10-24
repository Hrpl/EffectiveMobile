using System.Threading.Tasks;

namespace EffectiveMobile.Application.Services.Interfaces;

public interface IOrderServices
{
    public Task GetOrders(string _cityDistrict, string _firstDeliveryDateTime);
}
