using TempArAn.Domain.Exceptions.ExceptionHandlerMiddleware;

namespace TempAnAr.Extensions
{
    public static class ExceptionMiddlewareExtension
    {
        public static void UseApplicationExceptionHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<ApplicationExceptionHandlerMiddleware>();
        }
    }
}
