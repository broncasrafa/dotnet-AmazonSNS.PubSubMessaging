﻿using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using CloudNotes.Api.Middlewares;
using CloudNotes.Domain.Settings;
using CloudNotes.Infra.Data.DependencyInjection;
using CloudNotes.Application.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace CloudNotes.Api.Extensions;

public static class ConfigureServicesExtension
{
    public static void ConfigureServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.Configure<SNSSettings>(configuration.GetSection("EventPublishing"));
        services.Configure<S3Settings>(configuration.GetSection("Storage"));

        services.AddHttpContextAccessor();

        services.Configure<JsonOptions>(options => { options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull; });

        services.AddEndpointsApiExplorer();

        services.AddExceptionHandler<GlobalExceptionHandler>();

        services.AddProblemDetails();

        services.AddOpenApi(c =>
        {
            c.AddDocumentTransformer((document, _, _) =>
            {
                document.Info = new()
                {
                    Title = "CloudNotes.Api",
                    Version = "v1",
                    Description = "<br />Repositório: <a href='https://github.com/broncasrafa/dotnet-AmazonSNS.PubSubMessaging' target='_blank'>dotnet-AmazonSNS.PubSubMessaging</a>",
                    Contact = new()
                    {
                        Email = "broncasrafa@gmail.com",
                        Name = "Rafael Francisco",
                        Url = new Uri("https://github.com/broncasrafa")
                    }
                };

                //if (document.Tags == null)
                //    document.Tags = new List<OpenApiTag>();

                //document.Tags.Add(new OpenApiTag { Name = "Notes", Description = "Esta tag agrupa todos os endpoints relacionados ao gerenciamento de notas. Aqui você pode adicionar, listar, atualizar e remover notas." });

                return Task.CompletedTask;
            });

        });

        services.AddHealthChecks()
            .AddCheck("self", () => HealthCheckResult.Healthy(), ["api"]);

        services.AddInfrastructureData(configuration);

        services.AddApplication();
    }
}
