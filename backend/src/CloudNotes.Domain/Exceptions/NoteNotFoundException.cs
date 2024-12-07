using System.Net;
using CloudNotes.Domain.Exceptions.Common;

namespace CloudNotes.Domain.Exceptions;

public class NoteNotFoundException : BaseException
{
    public NoteNotFoundException(Guid noteId, HttpStatusCode statusCode = HttpStatusCode.InternalServerError) 
        : base($"Note with ID: '{noteId.ToString()}' was not found", statusCode)
    {
    }
}
