using CloudNotes.Application.DTO.ApiResult;
using CloudNotes.Application.DTO.Request;
using CloudNotes.Application.DTO.Response;

namespace CloudNotes.Application.Services.Interfaces;

public interface INoteService
{
    Task<Result<IEnumerable<NoteSummaryResponse>>> GetNoteListAsync(string username);
    Task<Result<NoteResponse>> GetNoteAsync(string username, Guid noteId);
    Task<Result<NoteResponse>> SaveNoteAsync(NoteCreateRequest request);
    Task<Result<NoteResponse>> UpdateNoteAsync(NoteUpdateRequest request);
    Task DeleteNoteAsync(string username, Guid noteId);
}
