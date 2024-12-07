namespace CloudNotes.Infra.Notification.LambdaEventProcessor.Models;

public enum EventType
{
    NoteCreated,
    NoteEdited,
    NoteViewed,
    NoteDeleted
}
