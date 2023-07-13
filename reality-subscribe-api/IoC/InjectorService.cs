using Application.Infra;
using Infra.DBConfiguration.EFCore;
using Infra.Repository;
using Models;
using reality_subscribe_api.Model;
using File = Models.File;

namespace reality_subscribe_api.IoC
{
    public static class InjectorService
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationContext>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //Repositorys
            services.AddScoped<IARepository<Subscribe>, SubscribeRepository>();
            services.AddScoped<IARepository<User>, UserRepository>();
            services.AddScoped<IARepository<SubscribeFile>, SubscribeFileRepository>();
            services.AddScoped<IARepository<File>, FileRepository>();

            services.AddHttpContextAccessor();
        }
    }
}
