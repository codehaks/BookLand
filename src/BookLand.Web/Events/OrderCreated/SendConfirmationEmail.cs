using MediatR;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace BookLand.Web.Events.OrderCreated;

public class SendConfirmationEmail : INotificationHandler<OrderCreatedNotification>
{
    private readonly IEmailSender _emailSender;
    private readonly ILogger<SendConfirmationEmail> _logger;
    public SendConfirmationEmail(IEmailSender emailSender, ILogger<SendConfirmationEmail> logger)
    {
        _emailSender = emailSender;
        _logger = logger;
    }

    public async Task Handle(OrderCreatedNotification notification, CancellationToken cancellationToken)
    {
        await Task.Delay(5000);
        await _emailSender.SendEmailAsync("email", "Confirmation", "Hello ... ");
        _logger.LogInformation("Confirmation email send to : " + notification.UserName);
    }
}

