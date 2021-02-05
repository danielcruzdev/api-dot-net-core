using AutoMapper;
using DependencyInjection;
using Helpers;
using Mapping;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Globalization;
using TemplateApi.Api.Infrastructure.Extensions;

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
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
                cfg.ValidateInlineMaps = false;
            });
            IMapper mapper = config.CreateMapper();

            services.AddSingleton(mapper);

            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc(name: "v1", new OpenApiInfo { Title = "api-dot-net-core", Version = "v1" });
            });


            services.ResolveDependencies();


            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddControllers();

            services.AddConfiguration<SettingsApplication>(Configuration, "SettingsApplication");
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            var supportedCultures = new[]
           {
                new CultureInfo("en-US"),
                new CultureInfo("fr"),
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("pt-BR"),
                // Formatting numbers, dates, etc.
                SupportedCultures = supportedCultures,
                // UI strings that we have localized.
                SupportedUICultures = supportedCultures
            });

            //app.UseRequestCultureHandler();
            app.UseProblemDetailsExceptionHandler();


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
