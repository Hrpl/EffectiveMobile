using EffectiveMobile.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace EffectiveMobile.API.Extensions;

public static class DbExtensions
{
    public static void AddDatabase(this WebApplicationBuilder builder)
    {

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(
                builder.Configuration.GetConnectionString("DbConnection"),
                o => o
                    .UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));

    }
}
