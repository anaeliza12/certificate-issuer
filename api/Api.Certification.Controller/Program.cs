using Api.Certification.Infra.ApiSettings.IoC;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.AddControllers();

builder.Services.RegisterServices(configuration);

var app = builder.Build();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();