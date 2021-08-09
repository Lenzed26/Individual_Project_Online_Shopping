using System;
using System.Collections.Generic;

#nullable disable

namespace ShopData
{
    public partial class Hunter
    {
        public Hunter()
        {
            Orders = new HashSet<Order>();
        }

        public int HunterId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
