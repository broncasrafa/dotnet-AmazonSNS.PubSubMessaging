namespace CloudNotes.Application.DTO.Request;

public record NoteUpdateRequest(Guid? Id, string Title, string Content);
