using System.Net;
using System.Text.Json;
using Sigmatech.Exceptions.Base;

namespace Sigmatech.Middleware;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILoggerFactory _loggerFactory;

    public GlobalExceptionMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
    {
        _next = next;
        _loggerFactory = loggerFactory;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(exception, true);

            // Get the default logger name
            ILogger _logger = _loggerFactory.CreateLogger("Exception");

            // If we have class that call the logger from the method then we use it as logger name
            if (trace.GetFrame(0).GetMethod().ReflectedType is not null)
            {
                _logger = _loggerFactory.CreateLogger(trace.GetFrame(0).GetMethod().ReflectedType.FullName);
            }
            
            var response = context.Response;
            response.ContentType = "application/json";
            var result = JsonSerializer.Serialize(new { });

            if (exception is BaseException ex)
            {
                response.StatusCode = ex.StatusCode;
                _logger.LogInformation(ex, $"{ex.Message}");
                result = JsonSerializer.Serialize(new { message = ex.Message, code = ex.Code, errors = ex.Errors });
            }
            else
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                _logger.LogError(exception, exception.Message);
                var objectError = new Dictionary<string, object>()
                {
                    {exception.Source , new List<string>(){exception.Message}}
                };
                result = JsonSerializer.Serialize(new { message = "Internal Server Error", code = "INTERNAL-SERVER-ERROR", errors = objectError });
            }
            
            await response.WriteAsync(result);
        }
    }
}