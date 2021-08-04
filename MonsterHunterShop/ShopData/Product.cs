using System;
using System.Collections.Generic;

#nullable disable

namespace ShopData
{
    public partial class Product
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public int Rarity { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
