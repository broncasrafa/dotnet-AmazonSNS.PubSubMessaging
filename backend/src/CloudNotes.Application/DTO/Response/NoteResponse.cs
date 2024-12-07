namespace CloudNotes.Application.DTO.Response;

public record NoteResponse(Guid? Id, string Title, string Content, DateTime CreatedAt);
