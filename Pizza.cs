using System;
using System.Collections.Generic;

#nullable disable

namespace PizzaShop
{
    public partial class Pizza
    {
        public Pizza()
        {
            OrdersDetails = new HashSet<OrdersDetail>();
        }

        public int PizzaNumber { get; set; }
        public string Name { get; set; }
        public int? Prise { get; set; }
        public string Type { get; set; }

        public virtual ICollection<OrdersDetail> OrdersDetails { get; set; }
    }
}
