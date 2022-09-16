using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using Application.Common.Exceptions;
using WebApi.Wrappers;

namespace WebApi.Middleware
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionHandlerMiddleware( RequestDelegate next)  => _next = next;
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            var result = string.Empty;
            switch (exception)
            {
                case ValidationException validationException:
                    code = HttpStatusCode.BadRequest;
                    //result = JsonSerializer.Serialize(validationException.Errors);
                    result = JsonSerializer.Serialize(new Response<string>(exception.Message));
                    break;
                case OpenPortFiledException:
                    code = HttpStatusCode.OK;
                    result = JsonSerializer
                        .Serialize(new Response<string>(exception.Message));
                    break;
                    //case NotFoundException:
                    //    code = HttpStatusCode.NotFound;
                    //    result = JsonSerializer.Serialize(new Response<string>("not found"));
                    //    break;
                    //case BadRequestException:
                    //    code = HttpStatusCode.BadRequest;
                    //    result = JsonSerializer.Serialize(new { error = exception.Message });
                    //    break;
            }
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            if (result == string.Empty)
            {
                //result = JsonSerializer.Serialize(new { error = exception.Message });
                result = JsonSerializer.Serialize(new Response<string>(exception.Message));
            }

            return context.Response.WriteAsync(result);
        }
    }
}
