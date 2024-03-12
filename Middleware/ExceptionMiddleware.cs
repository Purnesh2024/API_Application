using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Runtime.Serialization;

namespace API_Application.Middleware
{
    public class ExceptionMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning(ex, "Data Not found !!!.");
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                await context.Response.WriteAsync("Data not found.");
            }
            catch (NoContentException ex)
            {
                _logger.LogWarning(ex, "There is no content here !!!.");
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                await context.Response.WriteAsync("There is no content here.");
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, "Validation failed !!!.");
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await context.Response.WriteAsync(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred !!!.");
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync("An error occurred. Please try again later.");
            }
        }
    }

    [Serializable]
    internal class NoContentException : Exception
    {
        public NoContentException()
        {
        }

        public NoContentException(string? message) : base(message)
        {
        }

        public NoContentException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NoContentException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    [Serializable]
    internal class NotFoundException : Exception
    {
        public NotFoundException()
        {
        }

        public NotFoundException(string? message) : base(message)
        {
        }

        public NotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
