using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace api_dot_net_core
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                    .UseUrls("https://localhost:5001")
                    .UseStartup<Startup>();
                });
    }
}
