using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace Api.Processor
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            using var host = CreateHostBuilder(args).Build();
            var processor = host.Services.GetRequiredService<RabbitMQService>();
            processor.StartListening();

            await host.WaitForShutdownAsync();
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    var connection = "Server=DESKTOP-QIJ7H6I;Port=3306;Database=studentscertificate";
                    services.AddDbContext<MySQLContext>(options =>
                        options.UseMySql(connection, ServerVersion.AutoDetect(connection)));

                    services.AddSingleton<RabbitMQService>();
                    services.AddSingleton<CertificateProcessor>();
                });
    }
}
