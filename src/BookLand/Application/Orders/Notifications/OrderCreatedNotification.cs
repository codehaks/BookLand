using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLand.Application.Orders.Notifications;

public class OrderCreatedNotification: INotification
{
    public int BookId { get; set; }
    public string BookName { get; set; }
    public string UserName { get; set; }
}
