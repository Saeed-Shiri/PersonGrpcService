

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersonSrv.Domain.Repositories;
using PersonSrv.Infrastructure.Data;
using PersonSrv.Infrastructure.Repositories;

namespace PersonSrv.Infrastructure;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Add EF Core with SQLite
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

        // Register repositories
        services.AddScoped<IPersonRepository, PersonRepository>();

        return services;
    }
}