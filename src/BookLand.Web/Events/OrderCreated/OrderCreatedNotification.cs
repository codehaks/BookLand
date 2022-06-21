using MediatR;
using System.Net.NetworkInformation;

namespace BookLand.Web.Events.OrderCreated;

public class OrderCreatedNotification:INotification
{
    public string UserName { get; set; }
    public int OrderId { get; set; }
}

