using System;
using System.Collections.Generic;

#nullable disable

namespace PizzaShop
{
    public partial class Order
    {
        public Order()
        {
            OrdersDetails = new HashSet<OrdersDetail>();
        }

        public int OId { get; set; }
        public string UserId { get; set; }
        public int? Totall { get; set; }
        public string Deliverharge { get; set; }
        public string Status { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<OrdersDetail> OrdersDetails { get; set; }
    }
}
