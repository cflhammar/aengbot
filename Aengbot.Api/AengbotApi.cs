global using System;
global using System.Collections.Generic;
global using System.Threading;
global using System.Threading.Tasks;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Http;
global using static Microsoft.AspNetCore.Http.StatusCodes;
using Aengbot._2_Domain.Handlers;
using Aengbot.Handlers;
using Microsoft.AspNetCore.Http.HttpResults;
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

        app.MapPost("/subscribe", AddSubscription).Produces(Status200OK).Produces(Status400BadRequest);

        return app;
    }
    
    private static GetCoursesResult? GetAvailableCourses(CancellationToken ct)
    {
        var handler = new GetCoursesHandler(new CoursesProvider());
        return handler.Handle(ct);
    }
    
    private static IResult? AddSubscription(AddSubscriptionRequest request ,CancellationToken ct)
    {
        var command = new AddSubscriptionCommand(request.CourseId);
        
        var handler = new AddSubscriptionHandler();
        if (!handler.Handle(command, ct))
            return Results.BadRequest();

        return Results.Ok();
    }

    private record AddSubscriptionRequest(string CourseId, string FromTime, string ToTime, int NumberOfPlayers);
}
