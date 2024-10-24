using EffectiveMobile.Application.Services.Interfaces;
using SqlKata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EffectiveMobile.Application.Repositories.Interfaces;

public interface IAsyncRepository
{
    public Query GetQueryBuilder();
    public Task<IEnumerable<T>> GetListAsync<T>(Query query, CancellationToken ct);
}
