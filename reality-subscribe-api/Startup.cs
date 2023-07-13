using Application.Service;
using Config;
using Infra.AutoMapper;
using Infra.DBConfiguration.EFCore;
using MediatR;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using reality_subscribe_api.IoC;
using reality_subscribe_api.Middleware;
using Steeltoe.Extensions.Configuration.CloudFoundry;
using System.Reflection;

namespace reality_subscribe_api
{
    public class Startup
    {
        private readonly string MyAllowSpecificOrigins = "AllowOriginFrontendReality";
        public Microsoft.Extensions.Hosting.IHostEnvironment Environment { get; }
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration, Microsoft.Extensions.Hosting.IHostEnvironment environment)
        {
            Environment = environment;
            var builder = new ConfigurationBuilder()
                    .SetBasePath(environment.ContentRootPath)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true)
                    .AddCloudFoundry();
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureCloudFoundryOptions(Configuration);
            //services.AddAllActuators(Configuration);

            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                    policy =>
                    {
                        policy.WithOrigins(RealityCoreConfiguration.GetCorsOrigins().ToArray())
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                    });
            });

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Reality.WebApi", Version = "v1" });
                c.DescribeAllParametersInCamelCase();
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });

            services.AddMediatR(Assembly.GetExecutingAssembly(), typeof(IIntegrationService).Assembly);

            var databaseService = RealityCoreConfiguration.GetService(RealityCoreConfiguration.Service);
            services.AddDbContext<ApplicationContext>(o => o.UseSqlServer(databaseService.ConnectionString));

            services.RegisterServices();
            services.AddAutoMapper((mapper) =>
            {
                mapper.AddProfile<AutoMapperConfig>();
            });

            services.AddHealthChecks();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationContext dbContext)
        {

            app.UseForwardedHeaders();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Reality.WebApi v1"));
            }
            else
            {
                app.UseHsts();
            }

            dbContext.Database.Migrate();

            app.UseCors(MyAllowSpecificOrigins);

            //app.UseHttpsRedirection();

            app.UseMiddleware<ExceptionMiddleware>();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("health");
            });
        }

    }
}
