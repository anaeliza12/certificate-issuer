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
            services.Configure<RedisConfig>(configuration.GetSection(nameof(RedisConfig)));
            services.Configure<DBConfig>(configuration.GetSection(nameof(DBConfig)));

            services.AddSingleton(sp => sp.GetRequiredService<IOptions<RedisConfig>>().Value);
            services.AddSingleton(sp => sp.GetRequiredService<IOptions<DBConfig>>().Value);
            #endregion

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
