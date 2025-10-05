using System.Reflection;
using Application;
using Infrastructure;
using Serilog;
using Web.Api;
using Web.Api.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfig) => loggerConfig.ReadFrom.Configuration(context.Configuration));

builder.Services.AddSwaggerGenWithoutAuth();

builder.Services
    .AddApplication()
    .AddPresentation()
    .AddInfrastructure(builder.Configuration);

builder.Services.AddEndpoints(Assembly.GetExecutingAssembly());
builder.Services.AddHttpContextAccessor();

WebApplication app = builder.Build();

app.MapEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerWithUi();
    app.ApplyMigrations();

}

app.UseRequestContextLogging();

app.UseSerilogRequestLogging();

app.UseExceptionHandler();


app.MapControllers();

await app.RunAsync();
// REMARK: Required for functional and integration tests to work.

namespace Web.Api
{
    public partial class Program;
}
