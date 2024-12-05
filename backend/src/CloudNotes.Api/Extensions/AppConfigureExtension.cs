using HealthChecks.UI.Client;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace CloudNotes.Api.Extensions;

public static class AppConfigureExtension
{
    public static void Configure(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
            app.UseDeveloperExceptionPage();

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseExceptionHandler();

        app.MapOpenApi();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/openapi/v1.json", "CloudNotes.Api Swagger Docs");
            c.DisplayRequestDuration();
            c.DocExpansion(DocExpansion.None);
            c.EnableDeepLinking();
            c.ShowExtensions();
            c.ShowCommonExtensions();
        });

        app.MapHealthChecks("/hc", new() { Predicate = _ => true, ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse });
        app.MapHealthChecks("/liveness", new() { Predicate = r => r.Name.Contains("self", StringComparison.OrdinalIgnoreCase) });
        //app.MapHealthChecks(x => x.AsideMenuOpened = false);

        app.MapEndpoints();
    }
}
