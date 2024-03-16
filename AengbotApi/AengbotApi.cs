global using System;
global using System.Collections.Generic;
global using System.Threading;
global using System.Threading.Tasks;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Http;
global using static Microsoft.AspNetCore.Http.StatusCodes;
using Aengbot._2_Domain.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace AengbotApi;

internal static class AengbotAp
{
    public static WebApplication MapAengbotApi(this WebApplication app)
    {
        var api = app.NewVersionedApi();
        var v1 = api.MapGroup("/aengbot").HasApiVersion(1);

        app.MapGet("/courses", GetAvailableCourses).Produces<GetCoursesResult>(Status200OK).
            Produces(Status400BadRequest);

        return app;
    }


    private static GetCoursesResult? GetAvailableCourses(CancellationToken ct)
    {
        var handler = new GetCoursesHandler(new CoursesProvider());
        return handler.Handle(ct);
    }
}


//
// // Add services to the container.
// // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();
//
// var app = builder.Build();
//
// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }
//
// app.UseHttpsRedirection();
//
// app.MapGet("/courses", () =>
//     {
//
//     })
//     .WithOpenApi();
//
//
//
// app.Run();
