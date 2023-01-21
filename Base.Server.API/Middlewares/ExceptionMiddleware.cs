using Newtonsoft.Json;
using System.Net;

namespace Base.Server.API.Middlewares;

public class ExceptionMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(
        HttpContext context,
        Exception exception,
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
    {
        string result;
#if DEBUG
        result = JsonConvert.SerializeObject(new { error = exception.Message });
#else
		result = JsonConvert.SerializeObject(new { error = "An error occured while processing your request." });
#endif
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;
        return context.Response.WriteAsync(result);
    }
}