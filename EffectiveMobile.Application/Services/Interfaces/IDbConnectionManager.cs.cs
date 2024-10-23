using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EffectiveMobile.Application.Services.Interfaces;

public interface IDbConnectionManager
{
    public QueryFactory QueryFactory { get; }
}
