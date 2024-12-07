using CloudNotes.Domain.Entities.Common;

namespace CloudNotes.Domain.Entities;

public class Note : BaseEntity
{
    public string Title { get; set; }
    public string Content { get; set; }
    public string UserId { get; set; }    
}
