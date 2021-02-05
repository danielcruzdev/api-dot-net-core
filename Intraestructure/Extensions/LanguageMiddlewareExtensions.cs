using api_dot_net_core.Intraestructure.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace api_dot_net_core.Intraestructure.Extensions
{
    public static class LanguageMiddlewareExtensions
    {
        public static void UseRequestCultureHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<RequestCultureMiddleware>();
        }
    }
}
