using Reqnroll;
using Tests.Drivers;

namespace Tests.Steps;

[Binding]
public class DateTimeSteps(DateTimeDriver dateTimeDriver)
{
    [Given(@$"current date is (.*)")]
    public void GivenCurrentDateIs(DateTime dateTime)
    {
        dateTimeDriver.GivenCurrentDateIs(dateTime);
    }
}
