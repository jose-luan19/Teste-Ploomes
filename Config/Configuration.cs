using Newtonsoft.Json;
using System.Reflection;

namespace Config
{
    internal static class GlobalSettings
    {
        public static RealityCoreConfiguration Configuration { get; set; }
    }
    public class RealityCoreConfiguration
    {
        public const string Service = "Reality";
        public List<Service> Services { get; set; }
        public List<string> CorsOrigins { get; set; }

        public static Service GetService(string serviceName)
        {
            var settings = LoadJson();
            return settings.Services.FirstOrDefault(x => x.ServiceName == serviceName);
        }

        public static List<string> GetCorsOrigins()
        {
            return LoadJson()?.CorsOrigins;
        }

        private static RealityCoreConfiguration LoadJson()
        {
            RealityCoreConfiguration config = null;
            if (GlobalSettings.Configuration == null)
            {
                var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                var configPath = Path.Combine(path, "appsettings.Development.json");
                var reader = new JsonTextReader(new StringReader(File.ReadAllText(configPath)));
                var serializer = new JsonSerializer();
                config = serializer.Deserialize<RealityCoreConfiguration>(reader);
                GlobalSettings.Configuration = config;
            }

            return GlobalSettings.Configuration;
        }
    }

    public class Service
    {
        public string ServiceName { get; set; }
        public string ConnectionString { get; set; }
    }
}
