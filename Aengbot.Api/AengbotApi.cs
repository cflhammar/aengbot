global using System.Threading;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Http;
global using static Microsoft.AspNetCore.Http.StatusCodes;
using Aengbot.Handlers;
using Aengbot.Handlers.Command;
using Aengbot.Handlers.Query;

namespace AengbotApi;

internal static class AengbotAp
{
    public static WebApplication MapAengbotApi(this WebApplication app)
    {
        var api = app.NewVersionedApi();
        var v1 = api.MapGroup("/aengbot").HasApiVersion(1);

        v1.MapGet("/trigger", Trigger).Produces(Status200OK).Produces(Status400BadRequest);
        
        v1.MapGet("/courses", GetAvailableCourses).Produces(Status200OK, typeof(IResult))
            .Produces(Status400BadRequest);

        v1.MapPost("/subscribe", AddSubscription).Produces(Status200OK).Produces(Status400BadRequest);
        

        return app;
    }
    
    private static IResult Trigger(CancellationToken ct, ITriggerHandler handler)
    {
        var response = handler.Handle(ct);
        return Results.Ok(response);
    }

    private static async Task<IResult> GetAvailableCourses(CancellationToken ct, IGetCoursesHandler handler)
    {
        var response = await handler.Handle(ct);
        return Results.Ok(response);
    }

    private static IResult AddSubscription(AddSubscriptionRequest request, CancellationToken ct, IAddSubscriptionHandler handler)
    {
        var command = new AddSubscriptionCommand(
            request.CourseId, 
            request.Date, 
            request.FromTime, 
            request.ToTime,
            request.NumberOfPlayers, 
            request.Email);

        if (!handler.Handle(command, ct))
            return Results.BadRequest();

        return Results.Ok();
    }

    private record AddSubscriptionRequest(
        string CourseId,
        string Date,
        string FromTime,
        string ToTime,
        int NumberOfPlayers,
        string Email);
}