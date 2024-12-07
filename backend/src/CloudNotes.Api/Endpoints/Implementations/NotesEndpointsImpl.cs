using Microsoft.AspNetCore.Mvc;
using CloudNotes.Domain.Exceptions;
using CloudNotes.Application.DTO.Request;
using CloudNotes.Application.DTO.Response;
using CloudNotes.Application.DTO.ApiResult;
using CloudNotes.Application.Services.Interfaces;

namespace CloudNotes.Api.Endpoints.Implementations;

internal static class NotesEndpointsImpl
{
    public async static Task<IResult> GetAllNotes(ILogger<Program> logger, INoteService service)
    {
        logger.LogInformation("Getting all Notes");
        var response = await service.GetNoteListAsync(username: "");
        return TypedResults.Ok(Result<IEnumerable<NoteSummaryResponse>>.Success(response.Data));
    }
    public async static Task<IResult> GetOneNote(ILogger<Program> logger, INoteService service, Guid id)
    {
        logger.LogInformation($"Getting Note with ID: '{id}'");

        if (id == Guid.Empty)
        {
            logger.LogError($"Invalid Parameter with value: '{id}'");
            throw new InvalidParameterBadRequestException("Note ID is required");
        }

        var response = await service.GetNoteAsync(username: "", noteId: id);
        return TypedResults.Ok(Result<NoteResponse>.Success(response.Data));
    }
    public async static Task<IResult> PostNote([FromBody] NoteCreateRequest request, ILogger<Program> logger, INoteService service)
    {
        logger.LogInformation($"Creating new Note with title: '{request.Title}'");

        var response = await service.SaveNoteAsync(request);
        return TypedResults.CreatedAtRoute(routeName: "GetOneNote", routeValues: new { id = response.Data.Id }, value: Result<NoteResponse>.Success(response.Data));
    }
    public async static Task<IResult> PutNote([FromBody] NoteUpdateRequest request, ILogger<Program> logger, INoteService service, Guid id)
    {
        if (id == Guid.Empty)
        {
            logger.LogError($"Invalid Parameter with value: '{id}'");
            throw new InvalidParameterBadRequestException("Note ID is required");
        }

        if (id != request.Id)
        {
            var msgErr = $"Invalid Parameter with value: '{id}', does not match with body id value: '{request.Id}'";
            logger.LogError(msgErr);
            throw new InvalidParameterBadRequestException(msgErr);
        }

        logger.LogInformation($"Updating Note with ID: '{id}'");

        var response = await service.UpdateNoteAsync(request);
        return TypedResults.Ok(Result<NoteResponse>.Success(response.Data));
    }
    public async static Task<IResult> DeleteNote(ILogger<Program> logger, INoteService service, Guid id)
    {
        if (id == Guid.Empty)
        {
            logger.LogError($"Invalid Parameter with value: '{id}'");
            throw new InvalidParameterBadRequestException("Note ID is required");
        }

        logger.LogInformation($"Removing Note with ID: '{id}'");

        await service.DeleteNoteAsync(username: "", noteId: id);

        return TypedResults.Ok(Result<bool>.Success(true));
    }
}
