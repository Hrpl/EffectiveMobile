using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EffectiveMobile.Domain.Models;

public class OrderModel
{
    public long OrderNumber { get; set; }
    public DateTime OrderDate { get; set; }
    public string CityDistrict { get; set; }
    public double FloatWeight { get; set; }
}
