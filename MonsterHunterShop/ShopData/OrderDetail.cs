using System;
using System.Collections.Generic;

#nullable disable

namespace ShopData
{
    public partial class OrderDetail
    {
        public int OrderDetailsId { get; set; }
        public int? OrderId { get; set; }
        public int? ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }

        public override string ToString()
        {
            return $"[{OrderId},{ProductId}]: {Quantity} * {UnitPrice}z";
        }
    }
}
