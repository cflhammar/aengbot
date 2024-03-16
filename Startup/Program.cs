using Aengbot;
using AengbotApi;
using Asp.Versioning;
using Repository;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

using ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
ILogger logger = factory.CreateLogger("Program");

services.AddControllers();
services.AddSwaggerGen();
services.AddEndpointsApiExplorer();
services.AddDapperServices(configuration);
services.AddDomainServices(configuration);

services.AddEndpointsApiExplorer();
services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ApiVersionReader = new HeaderApiVersionReader("api-version");
}).AddApiExplorer(options => options.GroupNameFormat = "'v'VVV");

var app = builder.Build();
app.RunDbMigrations();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();
app.MapRestApi();
app.UseCors("AllowSpecificOrigins");


app.Run();


public partial class Program
{
}

