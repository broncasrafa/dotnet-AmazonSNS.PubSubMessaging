namespace CloudNotes.Application.DTO.Response;

public record NoteSummaryResponse(Guid? Id, string Title, string UserId, DateTime CreatedAt);