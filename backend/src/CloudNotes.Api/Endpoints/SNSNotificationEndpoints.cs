namespace CloudNotes.Api.Endpoints;

public static class SNSNotificationEndpoints
{
    public static IEndpointRouteBuilder MapSNSNotificationEndpoints(this IEndpointRouteBuilder builder)
    {
        var routes = builder.MapGroup("api/snsnotifications").WithTags("SNS Notification");



        return routes;
    }
}
