using WebApi.Middleware;

namespace WebApi.Middleware
{
    public static class CustomExceptionMiddlewareHandlerExtensions
    {
        public static void UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
        }
    }
}
