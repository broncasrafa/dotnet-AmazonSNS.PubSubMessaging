namespace CloudNotes.Api.Endpoints;

public static class NotesEndpoints
{
    public static IEndpointRouteBuilder MapNoteEndpoints(this IEndpointRouteBuilder builder)
    {
        var routes = builder.MapGroup("api/notes").WithTags("Notes");



        return routes;
    }
}
