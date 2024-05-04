using System.Net;

namespace MediatR.CommandQuery;

public class DomainException : Exception
{
    public DomainException()
        : this(HttpStatusCode.InternalServerError, "Unknown server error")
    {
    }

    public DomainException(string? message)
        : this(HttpStatusCode.InternalServerError, message)
    {
    }

    public DomainException(string? message, Exception? innerException)
        : this(HttpStatusCode.InternalServerError, message, innerException)
    {
    }

    public DomainException(HttpStatusCode statusCode, string? message)
        : base(message)
    {
        StatusCode = (int)statusCode;
    }

    public DomainException(HttpStatusCode statusCode, string? message, Exception? innerException)
        : base(message, innerException)
    {
        StatusCode = (int)statusCode;
    }

    public DomainException(int statusCode, string? message)
        : base(message)
    {
        StatusCode = statusCode;
    }

    public DomainException(int statusCode, string? message, Exception? innerException)
        : base(message, innerException)
    {
        StatusCode = statusCode;
    }


    public int StatusCode { get; }
}
