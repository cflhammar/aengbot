using System.Text.RegularExpressions;
using Aengbot.Models;
using Aengbot.Repositories;

namespace Aengbot.Handlers.Command;

public record AddCourseCommand(
    string Id,
    string Name);

public interface IAddCourseHandler : ICommandHandler
{
    Task<List<string>> Handle(AddCourseCommand command, CancellationToken ct);
}

public class AddCourseHandler(ICourseRepository repository) : IAddCourseHandler
{
    public async Task<List<string>> Handle(AddCourseCommand command, CancellationToken ct)
    {
        var errors = Validate(command, ct);
        if (errors.Count != 0) return errors;
        
        var course = new Course()
        {
            Id = command.Id,
            Name = command.Name
        };
        
        var result = await repository.AddCourse(course);;
        return result ? [] : ["Failed to add course"];
    }

    private List<string> Validate(AddCourseCommand command, CancellationToken ct)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(command.Id))
            errors.Add("Course id is required");
        
        if (string.IsNullOrWhiteSpace(command.Name))
            errors.Add("Course name is required");

        return errors;
    }
}