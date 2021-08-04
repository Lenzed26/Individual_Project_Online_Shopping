using System;
using System.Collections.Generic;
using System.Linq;
using ShopData;

namespace ShopBusiness
{
    public class ProductManager
    {

        public Product SelectedProduct { get; set; }

        public List<Product> RetrieveAllProducts()
        {
            using (var db = new MonsterHunterContext())
            {
                return db.Products.ToList();
            }
        }
    }
}
