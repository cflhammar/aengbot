using System.Net.Mail;
using System.Text.RegularExpressions;
using Aengbot.Notification;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Tests.Steps;

namespace Tests.Drivers;

public class NotificationsDriver
{
    private readonly EmailServiceMock _serviceMock = new();

    private class EmailServiceMock : IEmailService
    {
        internal readonly List<NotificationSteps.SentNotificationStep> SentNotifications = new();
        public void SendEmail(MailMessage message, string courseName)
        {
            // email example
            // Lediga tider hittade på Waxholm:
            // 2024/11/03 11:50  (2)
            // 2024/11/03 12:00  (3)

            var lines = message.Body.Split("\n").Where(s => s.Length > 5).ToList();
            var course = lines[0].Split(" ")[4].TrimEnd(':'); // will not capture full name if it contains spaces
            
            for (int i = 1; i < lines.Count; i++)
            {
                var line = lines[i];

                var regex = new Regex(Regex.Escape(" "));
                var newLine = regex.Replace(line, "T", 1);
                var splitLine = newLine.Split("  ");
                
                var date = DateTime.Parse(splitLine[0]);
                var slots = splitLine[1].Trim('(', ')');
                SentNotifications.Add(new NotificationSteps.SentNotificationStep
                {
                    Email = message.To.ToString(),
                    CourseName = course,
                    AvailableSlots = int.Parse(slots),
                    TeeTime = date
                });
            }
        }
        
    } 
    
    public void AddMockEmailService(IServiceCollection services)
    {
        services.RemoveAll(typeof(IEmailService));
        services.AddSingleton<IEmailService>(_serviceMock);
    }

    public void ThenNotificationsAreSentTo(List<NotificationSteps.SentNotificationStep> sentNotifications)
    {
        sentNotifications.Should().BeEquivalentTo(_serviceMock.SentNotifications);
    }
}