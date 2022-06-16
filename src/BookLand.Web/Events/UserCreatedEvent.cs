using BookLand.Migrations;
using MediatR;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Text.Encodings.Web;

namespace BookLand.Web.Events;

public class UserCreatedEvent
{
    public class Notification : INotification
    {

        public string Email { get; set; }
        public string CallbackUrl { get; set; }
    }

    public class NotificationHandler : INotificationHandler<Notification>
    {
        private readonly IEmailSender _emailSender;
        private readonly ILogger<UserCreatedEvent> _logger;
        public NotificationHandler(IEmailSender emailSender, ILogger<UserCreatedEvent> logger)
        {
            _emailSender = emailSender;
            _logger = logger;
        }

        public async Task Handle(Notification notification, CancellationToken cancellationToken)
        {
            await _emailSender.SendEmailAsync(notification.Email, "Confirm your email",
                      $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(notification.CallbackUrl)}'>clicking here</a>.");
            _logger.LogInformation("Email confimration sent to {email} ", notification.Email);

            // Send SMS
            // Notify admin
            //...
        }
    }
}