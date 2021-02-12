using api_dot_net_core.Intraestructure.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace TemplateApi.Api.Infrastructure.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void UseProblemDetailsExceptionHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
