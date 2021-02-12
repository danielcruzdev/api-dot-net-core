using api_dot_net_core.Repository.Concrete;
using api_dot_net_core.Repository.Contract;
using api_dot_net_core.Services.Concrete;
using api_dot_net_core.Services.Contract;
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

            services.AddScoped<IRComprasService, RComprasService>();
            services.AddScoped<IRCompraRepository, RCompraRepository>();

            return services;
        }
    }
}

