using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLand.Models;

public class Order
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public int Amount { get; set; }

    public int Quantity { get; set; }
    public string ShippingAddress { get; set; }
    public string UserId { get; set; }
    public string UserName{ get; set; }

    public DateTime TimeCreated { get; set; }
}
