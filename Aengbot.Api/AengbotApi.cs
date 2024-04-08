global using static Microsoft.AspNetCore.Http.StatusCodes;
using Aengbot.Handlers.Command;
using Aengbot.Handlers.Query;

namespace AengbotApi;

internal static class AengbotApi
{
    public static WebApplication MapAengbotApi(this WebApplication app)
    {
        var api = app.NewVersionedApi();
        var v1 = api.MapGroup("/aengbot").HasApiVersion(1);

        v1.MapGet("/wakeUp", () => Results.Ok("I'm awake!")).Produces(Status200OK).Produces(Status400BadRequest);
        
        v1.MapGet("/trigger", Trigger).Produces(Status200OK).Produces(Status400BadRequest);
        
        v1.MapGet("/courses", GetAvailableCourses).Produces(Status200OK, typeof(IResult))
            .Produces(Status400BadRequest);

        v1.MapPost("/subscribe", AddSubscription).Produces(Status200OK).Produces(Status400BadRequest);
        
        

        return app;
    }
    
    private static async Task<IResult> Trigger(CancellationToken ct, ITriggerHandler handler)
    {
        var response = await handler.Handle(ct);
        return Results.Ok("Notification sent to: " + string.Join(", ", response));
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
        DateTime FromTime,
        DateTime ToTime,
        int NumberOfPlayers,
        string Email);
}