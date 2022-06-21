using MediatR;

namespace BookLand.Web.Events.OrderCreated;

public class SendSmsToAdmin : INotificationHandler<OrderCreatedNotification>
{
    private readonly ILogger<SendSmsToAdmin> _logger;

    public SendSmsToAdmin(ILogger<SendSmsToAdmin> logger)
    {
        _logger = logger;
    }

    public async Task Handle(OrderCreatedNotification notification, CancellationToken cancellationToken)
    {
        await Task.Delay(2000);
        _logger.LogInformation("SMS send to admin");
    }
}
