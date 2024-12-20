﻿using EffectiveMobile.Application.Repositories.Implementations;
using EffectiveMobile.Application.Repositories.Interfaces;
using EffectiveMobile.Application.Services.Implementaions;
using EffectiveMobile.Application.Services.Interfaces;

namespace EffectiveMobile.API.Extensions;

public static class ServiceExtensions
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddRegisterServices();
    }

    public static void AddRegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IDbConnectionManager, DbConnectionManager>();
        services.AddScoped<IAsyncRepository, AsyncRepository>();
        services.AddScoped<IOrderServices, OrderServices>();
        services.AddScoped<IWriterService, WriterService>();
    }
}
