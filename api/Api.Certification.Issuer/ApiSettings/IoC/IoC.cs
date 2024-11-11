using Api.Certification.AppDomain.Interfaces;
using Api.Certification.AppDomain.Utils.AppSettings;
using Api.Certification.Infra.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Api.Certification.Infra.ApiSettings.Repositories.Context;
using Microsoft.EntityFrameworkCore;

namespace Api.Certification.Infra.ApiSettings.IoC
{
    public static class IoC
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            #region APPSETTINGS
            services.Configure<TemplateConfig>(configuration.GetSection("TemplateConfig"));
            services.AddSingleton(sp => sp.GetRequiredService<IOptions<TemplateConfig>>().Value);
            #endregion

            #region SERVICES
            services.AddTransient<IGenerateCertificateService, GenerateCertificateService>();

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
