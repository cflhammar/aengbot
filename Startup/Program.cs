using AengbotApi;
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

