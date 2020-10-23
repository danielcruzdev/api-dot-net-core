using Microsoft.Extensions.DependencyInjection;
using Repository;
using Service;

namespace DependencyInjection
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<IPessoaService, PessoaService>();
            services.AddScoped<IPessoaRepository, PessoaRepository>();

            return services;
        }
    }
}
