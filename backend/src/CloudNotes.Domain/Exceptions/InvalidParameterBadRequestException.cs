using System.Net;
using CloudNotes.Domain.Exceptions.Common;

namespace CloudNotes.Domain.Exceptions;

public class InvalidParameterBadRequestException : BaseException
{
    public InvalidParameterBadRequestException(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest) : base(message, statusCode)
    {
    }
}
