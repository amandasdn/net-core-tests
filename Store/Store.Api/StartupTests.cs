using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Store.Api
{
    public class StartupTests
    {
        public IConfiguration Configuration { get; }

        public StartupTests(IWebHostEnvironment hostEnvironment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(hostEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                //.AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }
    }
}
