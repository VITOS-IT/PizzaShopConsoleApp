using System;
using System.Collections.Generic;

#nullable disable

namespace PizzaShop
{
    public partial class OrdersDetail
    {
        public OrdersDetail()
        {
            OrderItemDetails = new HashSet<OrderItemDetail>();
        }

        public int ItemNumber { get; set; }
        public int? OId { get; set; }
        public int? PizzaNumber { get; set; }

        public virtual Order OIdNavigation { get; set; }
        public virtual Pizza PizzaNumberNavigation { get; set; }
        public virtual ICollection<OrderItemDetail> OrderItemDetails { get; set; }
    }
}
