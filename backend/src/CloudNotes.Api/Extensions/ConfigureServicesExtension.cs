using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using CloudNotes.Api.Middlewares;
using CloudNotes.Domain.Settings;

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
                return Task.CompletedTask;
            });
        });

        services.AddHealthChecks()
            .AddCheck("self", () => HealthCheckResult.Healthy(), ["api"]);
    }
}
