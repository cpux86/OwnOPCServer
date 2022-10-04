using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using Application.Common.Exceptions;
using Microsoft.AspNetCore.Http;
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
            var code = HttpStatusCode.OK;
            var result = string.Empty;
            switch (exception)
            {
                case OpenPortFiledException:
                    //code = HttpStatusCode.BadRequest;
                    result = JsonSerializer.Serialize(new { message = exception.Message });
                    break;
            }
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            if (result == string.Empty)
            {
                result = JsonSerializer.Serialize(new { error = exception.Message });
                //result = JsonSerializer.Serialize(new { message = "error" });
            }

            return context.Response.WriteAsync(result);
        }
    }
}
