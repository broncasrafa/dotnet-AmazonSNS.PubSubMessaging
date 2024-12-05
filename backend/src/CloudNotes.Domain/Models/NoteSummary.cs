namespace CloudNotes.Domain.Models;

public class NoteSummary
{
    public string UserId { get; set; }
    public Guid? NoteId { get; set; }
    public string Title { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
