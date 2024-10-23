using EffectiveMobile.Application.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;
using SqlKata.Compilers;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EffectiveMobile.Application.Services.Implementaions;

public class DbConnectionManager : IDbConnectionManager
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<DbConnectionManager> _logger;
    private string ConnectionString => _configuration["ConnectionStrings:DbConnection"];

    public DbConnectionManager(IConfiguration configuration, ILogger<DbConnectionManager> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    private NpgsqlConnection PostgresDbConnection => new(ConnectionString);
    public QueryFactory QueryFactory => new(PostgresDbConnection, new PostgresCompiler(), 60)
    {
        Logger = compiled => { _logger.LogInformation("Query = {@Query}", compiled.ToString()); }
    };
}
