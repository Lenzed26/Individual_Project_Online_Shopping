using System;
using System.Collections.Generic;

#nullable disable

namespace ShopData
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int OrderId { get; set; }
        public int? HunterId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? DeliveryDate { get; set; }

        public virtual Hunter Hunter { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public override string ToString()
        {
            return $"[{OrderId},{HunterId}]: {OrderDate}, {(DeliveryDate != null ? DeliveryDate: "Not delivered yet")}";
        }
    }
}
