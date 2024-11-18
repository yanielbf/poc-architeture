using System.Net;

namespace WROBoxLabelGeneration.Infrastructure.HttpClients.Exceptions
{
    public class HttpApiManagerException : HttpRequestException
    {
        public HttpStatusCode StatusCode { get; }
        public string Endpoint { get; }
        public string ClassName { get; }

        public HttpApiManagerException(string className, string endpoint, HttpStatusCode statusCode, string message)
            : base($"{message}, Class: {className}, Endpoint: {endpoint}, StatusCode: {statusCode}")
        {
            StatusCode = statusCode;
        }

        public override string ToString()
        {
            return $"{base.ToString()}, Class: {ClassName}, Endpoint: {Endpoint}, StatusCode: {StatusCode}";
        }
    }

    public class BadRequestException : HttpApiManagerException
    {
        public BadRequestException(string className, string endpoint, string message)
            : base(className, endpoint, HttpStatusCode.BadRequest, message) { }
    }

    public class UnauthorizedException : HttpApiManagerException
    {
        public UnauthorizedException(string className, string endpoint, string message)
            : base(className, endpoint, HttpStatusCode.Unauthorized, message) { }
    }

    public class ForbiddenException : HttpApiManagerException
    {
        public ForbiddenException(string className, string endpoint, string message)
            : base(className, endpoint, HttpStatusCode.Forbidden, message) { }
    }

    public class NotFoundException : HttpApiManagerException
    {
        public NotFoundException(string className, string endpoint, string message)
            : base(className, endpoint, HttpStatusCode.NotFound, message) { }
    }

    public class InternalServerErrorException : HttpApiManagerException
    {
        public InternalServerErrorException(string className, string endpoint, string message)
            : base(className, endpoint, HttpStatusCode.InternalServerError, message) { }
    }
}
