using System;
using System.Collections.Generic;

#nullable disable

namespace PizzaShop
{
    public partial class OrderItemDetail
    {
        public int Id { get; set; }
        public int? ItemNumber { get; set; }
        public int? ToppingNumber { get; set; }

        public virtual OrdersDetail ItemNumberNavigation { get; set; }
        public virtual Toping ToppingNumberNavigation { get; set; }
    }
}
