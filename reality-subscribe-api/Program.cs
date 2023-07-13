using Steeltoe.Common.Hosting;
using Steeltoe.Extensions.Configuration.CloudFoundry;

namespace reality_subscribe_api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseCloudHosting()
            .AddCloudFoundryConfiguration()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
            .ConfigureAppConfiguration((builderContext, configBuilder) =>
            {
                var env = builderContext.HostingEnvironment;
                configBuilder.SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddCloudFoundry();
            });

    }
}