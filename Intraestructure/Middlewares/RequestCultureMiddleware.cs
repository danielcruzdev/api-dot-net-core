using Microsoft.AspNetCore.Http;
using System.Globalization;
using System.Threading.Tasks;

namespace api_dot_net_core.Intraestructure.Middlewares
{
    public class RequestCultureMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestCultureMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            string language = "pt-BR";
            if (httpContext.Request.Headers.TryGetValue("Content-Language", out Microsoft.Extensions.Primitives.StringValues x))
            {
                language = x.ToString();
            }

            var culture = new CultureInfo(language);
            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;

            await _next(httpContext);
        }
    }
}
