﻿using CloudNotes.Domain.Enums;

namespace CloudNotes.Domain.Models;

public class Event
{
    public EventType EventType { get; set; }
    public Guid NoteId { get; set; }
    public DateTime TimeStamp { get; set; }
    public string Username { get; set; }

    public string GetSubject() => $"CloudNotes - {EventType.ToString()} - Note {NoteId}";
}
