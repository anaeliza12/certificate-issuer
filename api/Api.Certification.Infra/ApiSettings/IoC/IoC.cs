using Api.Certification.AppDomain.Interfaces;
using Api.Certification.AppDomain.Utils.AppSettings;
using Api.Certification.Infra.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Api.Certification.Infra.ApiSettings.Repositories.Context;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace Api.Certification.Infra.ApiSettings.IoC
{
    public static class IoC
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            #region APPSETTINGS
            services.Configure<TemplateConfig>(configuration.GetSection("TemplateConfig"));
            services.Configure<RedisConfig>(configuration.GetSection("RedisConfig"));

            services.AddSingleton(sp => sp.GetRequiredService<IOptions<RedisConfig>>().Value);
            services.AddSingleton(sp => sp.GetRequiredService<IOptions<TemplateConfig>>().Value);
            #endregion

            var redisConnection = configuration.GetConnectionString("RedisConnection");
            services.AddSingleton<ConnectionMultiplexer>(sp =>
                ConnectionMultiplexer.Connect("localhost:6379"));
            #region SERVICES
            services.AddTransient<IRedisService, RedisService>();
            services.AddTransient<IGenerateCertificateService, GenerateCertificateService>();
            services.AddTransient<IFindCertificateService, FindCertificateService>();


            services.AddDbContext<MySQLContext>(options =>
            options.UseMySql(configuration.GetConnectionString("DefaultConnection"),
            ServerVersion.AutoDetect(configuration.GetConnectionString("DefaultConnection"))));
            #endregion

            #region MEDIATOR CONFIG
            var assembly = System.AppDomain.CurrentDomain.Load("Api.Certification.AppDomain");
            services.AddMediatR(ctg => ctg.RegisterServicesFromAssemblies(assembly));
            #endregion
        }
    }
}
