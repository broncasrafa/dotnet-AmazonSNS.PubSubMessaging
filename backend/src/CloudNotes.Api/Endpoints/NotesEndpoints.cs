using Microsoft.AspNetCore.Mvc;
using CloudNotes.Api.Filters;
using CloudNotes.Api.Endpoints.Implementations;
using CloudNotes.Application.DTO.ApiResult;
using CloudNotes.Application.DTO.Response;

namespace CloudNotes.Api.Endpoints;

public static class NotesEndpoints
{
    public static IEndpointRouteBuilder MapNoteEndpoints(this IEndpointRouteBuilder builder)
    {
        var routes = builder.MapGroup("api/notes").WithTags("Notes");
                            

        routes.MapGet("/", NotesEndpointsImpl.GetAllNotes)
            .WithName("GetAllNotes")
            .Produces<Result<IEnumerable<NoteSummaryResponse>>>(StatusCodes.Status200OK)
            .WithDescription("Obter a lista de notas")
            .WithSummary("Obter a lista de notas")
            .WithOpenApi();

        routes.MapGet("/{id:Guid}", NotesEndpointsImpl.GetOneNote)
            .AllowAnonymous()
            .WithName("GetOneNote")
            .Produces<Result<NoteResponse>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .WithDescription("Obter a nota pelo ID especificado")
            .WithSummary("Obter a nota pelo ID especificado")
            .WithOpenApi();

        routes.MapPost("/", NotesEndpointsImpl.PostNote)
            .AddEndpointFilter<ValidationFilter>()
            .WithName("PostNote")
            .Produces<Result<NoteResponse>>(StatusCodes.Status201Created)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .WithDescription("Registrar uma nova nota")
            .WithSummary("Registrar uma nova nota")
            .WithOpenApi();

        routes.MapPut("/{id:Guid}", NotesEndpointsImpl.PutNote)
            .AddEndpointFilter<ValidationFilter>()
            .WithName("PutNote")
            .Produces<Result<NoteResponse>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .WithDescription("Atualizar os dados da nota pelo ID especificado")
            .WithSummary("Atualizar os dados da nota pelo ID especificado")
            .WithOpenApi();

        routes.MapDelete("/{id:Guid}", NotesEndpointsImpl.DeleteNote)
            .WithName("DeleteNote")
            .Produces<Result<bool>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .WithDescription("Deletar a nota pelo ID especificado")
            .WithSummary("Deletar a nota pelo ID especificado")
            .WithOpenApi();


        return routes;
    }
}
