using MediatR;
using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace BookLand.Application.Orders.Notifications;

public class SendEmailToBuyerHandler : INotificationHandler<OrderCreatedNotification>
{
    private readonly IMyEmailSender _emailSender;

    public SendEmailToBuyerHandler(IMyEmailSender emailSender)
    {
        _emailSender = emailSender;
    }

    public async Task Handle(OrderCreatedNotification notification, CancellationToken cancellationToken)
    {
        await _emailSender.SendEmailAsync(notification.UserName, "Receipt", "You bought a book " + notification.BookName);
    }
}

public class SendSmsToAdminHandler : INotificationHandler<OrderCreatedNotification>
{
    public async Task Handle(OrderCreatedNotification notification, CancellationToken cancellationToken)
    {
        // Send SMS

        await Task.Delay(1000);
        Console.WriteLine("SMS Sent.");

    }
}
