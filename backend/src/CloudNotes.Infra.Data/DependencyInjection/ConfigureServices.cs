using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CloudNotes.Domain.Interfaces.Repositories;
using CloudNotes.Domain.Interfaces.Repositories.Common;
using CloudNotes.Infra.Data.Repositories;
using CloudNotes.Infra.Data.Repositories.Common;
using CloudNotes.Infra.Data.Context;

namespace CloudNotes.Infra.Data.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureData(this IServiceCollection services, ConfigurationManager configuration)
    {
        AddDatabaseContext(services, configuration);
        AddRepositories(services);

        return services;
    }



    private static void AddDatabaseContext(IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlite(configuration.GetConnectionString("DatabaseConnection"),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName))
                      .EnableSensitiveDataLogging()
                      .EnableDetailedErrors();
        });
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        services.AddTransient<INoteRepository, NoteRepository>();
    }
}
