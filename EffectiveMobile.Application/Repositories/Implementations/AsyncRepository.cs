﻿using EffectiveMobile.Application.Repositories.Interfaces;
using EffectiveMobile.Application.Services.Interfaces;
using Microsoft.Extensions.Logging;
using SqlKata;
using SqlKata.Execution;

namespace EffectiveMobile.Application.Repositories.Implementations;

public class AsyncRepository : IAsyncRepository
{
    private QueryFactory _queryFactory;
    private readonly ILogger<AsyncRepository> _logger;

    public AsyncRepository(IDbConnectionManager dbConnection, ILogger<AsyncRepository> logger)
    {   
        _queryFactory = dbConnection.QueryFactory;
        _logger = logger;
    }
    public Query GetQueryBuilder(string tableName)
    {
        return _queryFactory.Query(tableName);
    }
    public async Task<IEnumerable<T>> GetListAsync<T>(Query query, CancellationToken ct = default)
    {
        return await query.GetAsync<T>(cancellationToken: ct);
    }
}
