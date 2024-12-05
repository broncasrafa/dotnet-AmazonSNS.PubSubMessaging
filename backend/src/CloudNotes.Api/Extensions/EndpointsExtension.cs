using CloudNotes.Api.Endpoints;

namespace CloudNotes.Api.Extensions;

public static class EndpointsExtension
{
    public static IApplicationBuilder MapEndpoints(this WebApplication app)
    {
        app.MapNoteEndpoints();
        app.MapSNSNotificationEndpoints();

        return app;
    }
}
