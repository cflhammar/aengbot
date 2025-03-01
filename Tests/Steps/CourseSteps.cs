using Reqnroll;
using Tests.Drivers;

namespace Tests.Steps;

[Binding]
public class CourseSteps(CourseDriver courseDriver)
{
    [Given(@"courses exist")]
    public async Task GivenCoursesExist(Table table)
    {
        var courses = table.CreateSet<CourseStep>().ToList();
        foreach (var course in courses)
        {
            await courseDriver.AddCourse(course);
        }
    }

    public record CourseStep(string Id, string Name);
}