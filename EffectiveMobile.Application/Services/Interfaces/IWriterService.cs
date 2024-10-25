using EffectiveMobile.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EffectiveMobile.Application.Services.Interfaces;

public interface IWriterService
{
    public void WriteToFile(List<OrderModel> orders);
}
