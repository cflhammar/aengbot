using Aengbot;
using Aengbot.Notification;
using AengbotApi;
using Asp.Versioning;
using Microsoft.OpenApi.Models;
using Repository;
using Sweetspot.External.Api;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

using ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
ILogger logger = factory.CreateLogger("Program");


//
// services.AddCors(options =>
// {
//     options.AddDefaultPolicy(
//         policy  =>
//         {
//             // policy.WithOrigins("*");
//             // policy.WithHeaders("Origin", "Content-Type", "XAPIKEY");
//             // policy.WithMethods("GET", "POST", "OPTIONS");
//             policy.AllowAnyOrigin();
//             policy.AllowAnyHeader();
//             policy.AllowAnyMethod();
//
//         });
// });

services.AddControllers();
services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("apiKey", new OpenApiSecurityScheme()
    {
        Description = "API KEY",
        Name = "XAPIKEY",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "apiKey"
                }
            },
            Array.Empty<string>()
        },
    });

});
services.AddEndpointsApiExplorer();
services.AddDapperServices(configuration);
services.AddDomainServices(configuration);

services.AddScoped<IEmailService,EmailService>();
services.AddScoped<INotifier,Notifier>();
services.AddHttpClient<ISweetSpotApi, SweetSpotApi>();

services.AddEndpointsApiExplorer();
services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ApiVersionReader = new HeaderApiVersionReader("api-version");
}).AddApiExplorer(options => options.GroupNameFormat = "'v'VVV");

var app = builder.Build();
app.UseCors(options =>
{
    options.AllowAnyOrigin();
    options.AllowAnyHeader();
    options.AllowAnyMethod();
});
app.RunDbMigrations();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
    
});

app.UseHttpsRedirection();
app.MapRestApi();

app.Run();

public partial class Program
{
}

