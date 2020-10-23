using AutoMapper;
using DependencyInjection;
using Helpers;
using Mapping;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace api_dot_net_core
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            #region AUTO MAPPER

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
                cfg.ValidateInlineMaps = false;
            });
            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            #endregion

            #region SWAGGER
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc(name: "v1", new OpenApiInfo { Title = "api-dot-net-core", Version = "v1" });
            });
            #endregion

            #region DEPENDENCY INJECTION

            services.ResolveDependencies();

            #endregion

            services.AddConfiguration<SettingsApplication>(Configuration, "SettingsApplication");

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            app.UseSwagger(option =>
            {
                option.RouteTemplate = "swagger/{documentName}/swagger.json";
            });

            app.UseSwaggerUI(c =>
               {
                   c.SwaggerEndpoint(url: "v1/swagger.json", name: "API");
               }
            );

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
