using ChatRoom.Api.Contracts.Wrappers;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;

namespace ChatRoom.Api.Infrastructure.Middlewares;


public class CustomGlobalErrorHandlerMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            var responseModel = new BaseResult<string>(new Error(ErrorCode.Exception, error?.Message));

            switch (error)
            {
                case KeyNotFoundException e:
                    // not found error

                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                default:
                    // unhandled error
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }
            var result = JsonSerializer.Serialize(responseModel);

            await response.WriteAsync(result);
        }
    }
}






