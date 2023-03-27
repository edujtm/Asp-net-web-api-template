using Contracts;
using LoggerService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Repository;
using Service.Contracts;
using Services;

namespace Asp_net_web_api_template.Extensions
{
    public static class ServiceExtensions
    {

        public static void ConfigureCors(this IServiceCollection services) =>
         services.AddCors(options =>
         {
             options.AddPolicy("Cors", builder =>
             builder.AllowAnyOrigin()
             .AllowAnyMethod()
             .AllowAnyHeader()
             .WithExposedHeaders("X-Pagination"));
         });

        public static void ConfigureLoggerService(this IServiceCollection services) =>
            services.AddSingleton<ILoggerManager, LoggerManager>();

        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
            services.AddScoped<IRepositoryManager, RepositoryManager>();

        public static void ConfigureServiceManager(this IServiceCollection services) =>
            services.AddScoped<IServiceManager, ServiceManager>();

        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
            services.AddDbContext<RepositoryContext>(opts =>
                opts.UseSqlServer(configuration.GetConnectionString("sqlConnection")));

        public static void ConfigureVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(opt =>
            {
                opt.ReportApiVersions = true;
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.DefaultApiVersion = new ApiVersion(1, 0);
                opt.ApiVersionReader = new HeaderApiVersionReader("api-version");
            });
        }



    }
}
