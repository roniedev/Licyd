using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;

namespace BaseLib;

/// <summary>
/// 
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class NotificationFilter : Attribute, IAsyncResultFilter
{
    private readonly ResponseObject _responseObject;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="responseObject"></param>
    public NotificationFilter(ResponseObject responseObject)
    {
        _responseObject = responseObject;
    }

    /// <summary>
    /// Result of the request
    /// </summary>
    /// <param name="context"></param>
    /// <param name="next"></param>
    /// <returns></returns>
    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        if (_responseObject.HasNotifications)
        {
            context.HttpContext.Response.StatusCode = (int)_responseObject.StatusCode;
            context.HttpContext.Response.ContentType = "application/json";

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var jsonBytes = JsonSerializer.SerializeToUtf8Bytes(_responseObject.Notifications, options);
            await context.HttpContext.Response.Body.WriteAsync(jsonBytes);

            return;
        }

        await next();
    }
}
