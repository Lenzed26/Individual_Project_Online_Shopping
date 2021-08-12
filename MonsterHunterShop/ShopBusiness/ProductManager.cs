using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.EntityFrameworkCore;
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

        public void SetSelectedProducted(object selectedItem)
        {
            SelectedProduct = (Product)selectedItem;
        }

        public void Create(string productName, string category, int rarity, decimal price, string description = null)
        {
            var newProduct = new Product() { ProductName = productName, Category = category, Rarity = rarity, UnitPrice = price, Description = description };
            using (var db = new MonsterHunterContext())
            {                
                if (CheckDuplicate(productName) == true)
                {
                    db.Products.Add(newProduct);
                    db.SaveChanges();
                }
                else
                {
                    throw new ArgumentException("Item already exists");
                }
            }
        }

        public bool Update(string productName, string category, int rarity, decimal price, string description = null)
        {
            using (var db = new MonsterHunterContext())
            {                
                if(CheckDuplicate(productName) == true)
                {
                    return false;
                }
                SelectedProduct.Category = category;
                SelectedProduct.Rarity = rarity;
                SelectedProduct.UnitPrice = price;
                if (SelectedProduct.Description == null)
                {
                    SelectedProduct.Description = description;
                }
                db.SaveChanges();
            }
            return true;
        }
        public bool Update(string oldProductName, string newProductName, string category, int rarity, decimal price, string description = null)
        {
            using (var db = new MonsterHunterContext())
            {                
                if (CheckDuplicate(newProductName) == true)
                {
                    return false;
                }
                SelectedProduct.ProductName = newProductName;
                SelectedProduct.Category = category;
                SelectedProduct.Rarity = rarity;
                SelectedProduct.UnitPrice = price;
                if (SelectedProduct.Description == null)
                {
                    SelectedProduct.Description = description;
                }
                db.SaveChanges();
            }
            return true;
        }

        public bool Delete(string productName)
        {
            using (var db = new MonsterHunterContext())
            {
                SelectedProduct = db.Products.Where(p => p.ProductName == productName).FirstOrDefault();
                if (SelectedProduct == null)
                {
                    Debug.WriteLine($"Product {productName} is not found");
                    return false;
                }

                db.Remove(SelectedProduct);

                var RemoveFromOrderDetails = db.OrderDetails.Include(p => p.Product).Where(p => p.Product.ProductName.Equals(productName)).FirstOrDefault();
                if (RemoveFromOrderDetails != null) db.Remove(RemoveFromOrderDetails);

                db.SaveChanges();
            }
            return true;
        }

        public bool CheckDuplicate(string productName)
        {
            using (var db = new MonsterHunterContext())
            {
                var query = db.Products.Where(p => p.ProductName.Equals(productName)).FirstOrDefault();
                if (query == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
    }
}
