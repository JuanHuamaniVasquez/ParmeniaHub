using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ParmeniaHub.Application.Convocatorias;
using ParmeniaHub.Infrastructure.Persistence;
using ParmeniaHub.Infrastructure.Persistence.Repositories;

namespace ParmeniaHub.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException(
                "La cadena de conexión 'DefaultConnection' no está configurada.");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddScoped<IConvocatoriaRepository, ConvocatoriaRepository>();

        return services;
    }
}
