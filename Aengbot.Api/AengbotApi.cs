global using static Microsoft.AspNetCore.Http.StatusCodes;
using Aengbot.Handlers.Command;
using Aengbot.Handlers.Query;
using Microsoft.AspNetCore.Mvc;

namespace AengbotApi;

internal static class AengbotApi
{
    public static WebApplication MapAengbotApi(this WebApplication app)
    {
        var api = app.NewVersionedApi();
        var v1 = api.MapGroup("/aengbot").HasApiVersion(1);

        v1.MapGet("/wakeUp", () => Results.Ok("I'm awake!")).Produces(Status200OK).Produces(Status400BadRequest);
        
        v1.MapGet("/trigger", Trigger).Produces(Status200OK).Produces(Status400BadRequest);

        // courses
        v1.MapPost("/courses/add", AddCourse).Produces(Status200OK, typeof(IResult))
            .Produces(Status400BadRequest);
        
        v1.MapGet("/courses", GetCourses).Produces(Status200OK, typeof(IResult))
            .Produces(Status400BadRequest);

        // subs
        v1.MapPost("subscriptions/add", AddSubscription).Produces(Status200OK).Produces(Status400BadRequest);
        
        // get subs with email as query param
        v1.MapGet("subscriptions/{email}", GetSubscriptions).Produces(Status200OK).Produces(Status400BadRequest);
        
        

        return app;
    }

    private static async Task<IResult> Trigger(CancellationToken ct, ITriggerHandler handler)
    {
        var response = await handler.Handle(ct);
        return Results.Ok("Notification sent to: " + string.Join(", ", response));
    }
    
    private static async Task<IResult> AddCourse(AddCourseRequest request, CancellationToken ct, IAddCourseHandler handler)
    {
        var command = new AddCourseCommand(
            request.Id,
            request.Name
            );

        var errors = await handler.Handle(command, ct);
        if (errors.Count != 0)
            return Results.BadRequest(errors);

        return Results.Ok("Subscription added successfully");
    }
    
    private static async Task<IResult> GetCourses(CancellationToken ct, IGetCoursesHandler handler)
    {
        var response = await handler.Handle(ct);
        return Results.Ok(response);
    }

    private static async Task<IResult> GetSubscriptions([FromRoute] string email, CancellationToken ct, IGetSubscriptionsHandler handler)
    {
        var response = await handler.Handle(email, ct);
        return Results.Ok(response);
    }
    
    private static async Task<IResult> AddSubscription(AddSubscriptionRequest request, CancellationToken ct, IAddSubscriptionHandler handler)
    {
        var command = new AddSubscriptionCommand(
            request.CourseId, 
            request.FromTime, 
            request.ToTime,
            request.NumberOfPlayers, 
            request.Email);

        var errors = await handler.Handle(command, ct);
        if (errors.Count != 0)
            return Results.BadRequest(errors);

        return Results.Ok("Subscription added successfully");
    }

    private record AddSubscriptionRequest(
        string CourseId,
        DateTime FromTime,
        DateTime ToTime,
        int NumberOfPlayers,
        string Email);
    
    private record AddCourseRequest(
        string Id,
        string Name);
}