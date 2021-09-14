using System;
using System.Collections.Generic;

#nullable disable

namespace PizzaShop
{
    public partial class Toping
    {
        public Toping()
        {
            OrderItemDetails = new HashSet<OrderItemDetail>();
        }

        public int ToppingNumber { get; set; }
        public string Name { get; set; }
        public int? Prise { get; set; }

        public virtual ICollection<OrderItemDetail> OrderItemDetails { get; set; }
    }
}
