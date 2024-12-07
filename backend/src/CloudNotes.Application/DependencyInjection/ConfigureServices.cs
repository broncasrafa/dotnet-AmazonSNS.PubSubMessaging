using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using CloudNotes.Application.Services.Implementations;
using CloudNotes.Application.Services.Interfaces;
using CloudNotes.Application.Mappers;
using FluentValidation;

namespace CloudNotes.Application.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        AddServices(services);
        AddAutomapper(services);
        AddFluentValidation(services);

        return services;
    }

    private static void AddServices(IServiceCollection services)
    {
        services.AddScoped<INoteService, NoteService>();
    }

    private static void AddAutomapper(IServiceCollection services) => services.AddAutoMapper(typeof(MappingProfile));
    private static void AddFluentValidation(IServiceCollection services) => services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
}
