using NUnit.Framework;
using ShopBusiness;
using ShopData;
using System.Linq;
using System.Data;
using System;
using Microsoft.EntityFrameworkCore;

namespace ShopTests
{
    public class DataLayerTests
    {
        /// <summary>
        /// Unit Tests to check if database works, using entity framework and linq only
        /// </summary>

        static string _name, _location, _category;
        static int _rarity;
        static decimal _price;
        static DateTime _date;
        [SetUp]
        public void SetUp()
        {

            using (var db = new MonsterHunterContext())
            {
                //Finding and removing test input for each test
                var testHunter = db.Hunters.Where(h => h.Name == "Test Hunter");

                var testHunterId = 0;
                testHunter.ToList().ForEach(i => testHunterId = i.HunterId);

                var testOrder = db.Orders.Where(h => h.HunterId == testHunterId);

                var testOrderDetailsID = db.OrderDetails.Include(p => p.Product).Where(p => p.Product.ProductName == "Potion").Select(i => i.OrderDetailsId).FirstOrDefault();
                var testOrderDetail = db.OrderDetails.Where(n => n.OrderDetailsId == testOrderDetailsID);
                var testProduct = db.Products.Where(p => p.ProductName == "Test Product");

                db.OrderDetails.RemoveRange(testOrderDetail);
                db.Orders.RemoveRange(testOrder);
                db.Hunters.RemoveRange(testHunter);
                db.Products.RemoveRange(testProduct);
                db.SaveChanges();
            }
        }

        [Test]
        [Category("Happy")]
        [Category("Hunter")]
        public void CreateUser_Hunter_CountIncreasesByOne()
        {
            _name = "Test Hunter";
            _location = "Debug Field";
            using (var db = new MonsterHunterContext())
            {
                var beforeCount = db.Hunters.Count();
                db.Hunters.Add(new Hunter() { Name = _name, Location = _location });
                db.SaveChanges();
                var afterCount = db.Hunters.Count();

                Assert.That(afterCount - 1, Is.EqualTo(beforeCount));

            }
        }

        [Test]
        [Category("Happy")]
        [Category("Hunter")]
        public void CreateUser_Hunter_QueryReturnsCorrectDetails()
        {
            _name = "Test Hunter";
            _location = "Debug Field";
            using (var db = new MonsterHunterContext())
            {
                db.Hunters.Add(new Hunter() { Name = _name, Location = _location });
                db.SaveChanges();
                var query =
                    from h in db.Hunters
                    where h.Name == _name
                    select h;

                foreach (var h in query)
                {
                    Assert.That(h.Name, Is.EqualTo(_name));
                    Assert.That(h.Location, Is.EqualTo(_location));
                }
            }
        }

        [Test]
        [Category("Happy")]
        [Category("Hunter")]
        public void DeleteUser_Hunter_CountDecreasesByOne()
        {
            _name = "Test Hunter";
            _location = "Debug Field";
            using (var db = new MonsterHunterContext())
            {
                db.Hunters.Add(new Hunter() { Name = _name, Location = _location });
                db.SaveChanges();
                var remove = db.Hunters.Where(h => h.Name.Equals(_name)).FirstOrDefault();
                var beforeCount = db.Hunters.Count();
                db.Hunters.Remove(remove);
                db.SaveChanges();
                var afterCount = db.Hunters.Count();

                Assert.That(afterCount + 1, Is.EqualTo(beforeCount));
            }
        }

        [Test]
        [Category("Sad")]
        [Category("Hunter")]
        public void DeleteUser_Hunter_QueryReturnsNull()
        {
            _name = "Test Hunter";
            _location = "Debug Field";
            using (var db = new MonsterHunterContext())
            {
                //Creating and removing test hunter
                db.Hunters.Add(new Hunter() { Name = _name, Location = _location });
                db.SaveChanges();
                var remove = db.Hunters.Where(h => h.Name.Equals(_name)).FirstOrDefault();
                db.Hunters.Remove(remove);
                db.SaveChanges();

                //Query for deleted test hunter
                var query = db.Hunters.Where(h => h.Name.Equals(_name)).FirstOrDefault();

                Assert.That(query, Is.Null);

            }
        }

        [Test]
        [Category("Product")]
        [Category("Happy")]
        public void CreateProduct_Products_CountIncreasesByOne()
        {
            _name = "Test Product";
            _category = "Test";
            _rarity = 99;
            _price = 101;

            using (var db = new MonsterHunterContext())
            {
                var beforeCount = db.Products.Count();
                db.Products.Add(new Product() { ProductName = _name, UnitPrice = _price, Category = _category, Rarity = _rarity });
                db.SaveChanges();
                var afterCount = db.Products.Count();

                Assert.That(afterCount - 1, Is.EqualTo(beforeCount));
            }
        }

        [Test]
        [Category("Product")]
        [Category("Happy")]
        public void CreateProduct_Product_QueryReturnsCorrectDetails()
        {
            _name = "Test Product";
            _category = "Test";
            _rarity = 99;
            _price = 101;

            using (var db = new MonsterHunterContext())
            {
                db.Products.Add(new Product() { ProductName = _name, UnitPrice = _price, Category = _category, Rarity = _rarity });
                db.SaveChanges();

                var query = db.Products.Where(p => p.ProductName == _name).FirstOrDefault();

                Assert.That(query.ProductName, Is.EqualTo(_name));
                Assert.That(query.Category, Is.EqualTo(_category));
                Assert.That(query.Rarity, Is.EqualTo(_rarity));
                Assert.That(query.UnitPrice, Is.EqualTo(_price));
                Assert.That(query.Description, Is.Null);
            }
        }

        [Test]
        [Category("Happy")]
        [Category("Product")]
        public void DeleteProduct_Product_CountDecreasesByOne()
        {
            _name = "Test Product";
            _category = "Test";
            _rarity = 99;
            _price = 101;

            using (var db = new MonsterHunterContext())
            {
                db.Products.Add(new Product() { ProductName = _name, UnitPrice = _price, Category = _category, Rarity = _rarity });
                db.SaveChanges();
                var beforeCount = db.Products.Count();
                var query = db.Products.Where(p => p.ProductName == _name).FirstOrDefault();

                db.Products.Remove(query);
                db.SaveChanges();
                var afterCount = db.Products.Count();

                Assert.That(afterCount + 1, Is.EqualTo(beforeCount));
            }
        }

        [Test]
        [Category("Sad")]
        [Category("Product")]
        public void DeleteProduct_Product_QueryReturnsNull()
        {
            _name = "Test Product";
            _category = "Test";
            _rarity = 99;
            _price = 101;

            using (var db = new MonsterHunterContext())
            {
                db.Products.Add(new Product() { ProductName = _name, UnitPrice = _price, Category = _category, Rarity = _rarity });
                db.SaveChanges();

                var query = db.Products.Where(p => p.ProductName == _name).FirstOrDefault();

                db.Products.Remove(query);
                db.SaveChanges();

                query = db.Products.Where(p => p.ProductName == _name).FirstOrDefault();
                Assert.That(query, Is.Null);
            }
        }

        [Test]
        [Category("Orders")]
        [Category("Happy")]
        public void CreateOrder_Order_CountIncreasesByOne()
        {
            _name = "Test Hunter";
            _location = "Debug Field";
            _date = DateTime.Now;

            using (var db = new MonsterHunterContext())
            {
                //Create Hunter
                db.Hunters.Add(new Hunter() { Name = _name, Location = _location });
                db.SaveChanges();
                var newHunterID = db.Hunters.Where(p => p.Name == "Test Hunter").Select(i => i.HunterId).FirstOrDefault();
                //Create Order
                var beforeCount = db.Orders.Count();
                db.Orders.Add(new Order() {HunterId = newHunterID, OrderDate = _date});
                db.SaveChanges();
                //Check for count
                var afterCount = db.Orders.Count();

                Assert.That(afterCount - 1, Is.EqualTo(beforeCount));
            }
        }

        [Test]
        [Category("Orders")]
        [Category("Happy")]
        public void CreateOrder_Order_QueryReturnsCorrectDetails()
        {
            _name = "Test Hunter";
            _location = "Debug Field";
            _date = DateTime.Now;

            using (var db = new MonsterHunterContext())
            {
                //Create Hunter
                db.Hunters.Add(new Hunter() { Name = _name, Location = _location });
                db.SaveChanges();
                var newHunterID = db.Hunters.Where(p => p.Name == "Test Hunter").Select(i => i.HunterId).FirstOrDefault();
                //Create Order
                db.Orders.Add(new Order() { HunterId = newHunterID, OrderDate = _date });
                db.SaveChanges();

                var query = db.Orders.Where(h => h.HunterId == newHunterID).FirstOrDefault();

                Assert.That(query.HunterId, Is.EqualTo(newHunterID));
                Assert.That(query.OrderDate, Is.EqualTo(_date));
                Assert.That(query.DeliveryDate, Is.Null);
            }
        }

        [Test]
        [Category("Orders")]
        [Category("Happy")]
        public void DeleteOrder_Order_CountDecreasesByOne()
        {
            _name = "Test Hunter";
            _location = "Debug Field";
            _date = DateTime.Now;

            using (var db = new MonsterHunterContext())
            {
                //Create Hunter
                db.Hunters.Add(new Hunter() { Name = _name, Location = _location });
                db.SaveChanges();
                var newHunterID = db.Hunters.Where(p => p.Name == "Test Hunter").Select(i => i.HunterId).FirstOrDefault();
                //Create Order                
                db.Orders.Add(new Order() { HunterId = newHunterID, OrderDate = _date });                
                db.SaveChanges();
                var beforeCount = db.Orders.Count();

                //Delete Order
                var query = db.Orders.Where(i => i.HunterId == newHunterID).FirstOrDefault();
                db.Orders.Remove(query);
                db.SaveChanges();
                //Check for count
                var afterCount = db.Orders.Count();

                Assert.That(afterCount + 1, Is.EqualTo(beforeCount));
            }
        }

        [Test]
        [Category("Orders")]
        [Category("Sad")]
        public void DeleteOrder_Order_QueryReturnsNull()
        {
            _name = "Test Hunter";
            _location = "Debug Field";
            _date = DateTime.Now;

            using (var db = new MonsterHunterContext())
            {
                //Create Hunter
                db.Hunters.Add(new Hunter() { Name = _name, Location = _location });
                db.SaveChanges();
                var newHunterID = db.Hunters.Where(p => p.Name == "Test Hunter").Select(i => i.HunterId).FirstOrDefault();
                //Create Order
                db.Orders.Add(new Order() { HunterId = newHunterID, OrderDate = _date });
                db.SaveChanges();

                //Delete Order
                var query = db.Orders.Where(i => i.HunterId == newHunterID).FirstOrDefault();
                db.Orders.Remove(query);
                db.SaveChanges();

                query = db.Orders.Where(h => h.HunterId == newHunterID).FirstOrDefault();

                Assert.That(query, Is.Null);
            }
        }

        [Test]
        [Category("OrderDetails")]
        [Category("Happy")]
        public void CreateOrderDetail_OrderDetails_CountIncreasesByOne()
        {
            _name = "Test Hunter";
            _location = "Debug Field";
            _date = DateTime.Now;

            using (var db = new MonsterHunterContext())
            {
                //Create Hunter
                db.Hunters.Add(new Hunter() { Name = _name, Location = _location });
                db.SaveChanges();
                var newHunterID = db.Hunters.Where(p => p.Name == "Test Hunter").Select(i => i.HunterId).FirstOrDefault();

                //Create Order
                db.Orders.Add(new Order() { HunterId = newHunterID, OrderDate = _date });
                db.SaveChanges();
                var newOrderID = db.Orders.Where(o => o.HunterId == newHunterID).Select(i => i.OrderId).FirstOrDefault();

                //Create Order Detail
                var productDetails = db.Products.Where(p => p.ProductName == "Potion").Select(o => o).FirstOrDefault();

                var beforeCount = db.OrderDetails.Count();
                db.OrderDetails.Add(new OrderDetail() { OrderId = newOrderID, ProductId = productDetails.ProductId, Quantity = 10, UnitPrice = productDetails.UnitPrice });
                db.SaveChanges();
                var afterCount = db.OrderDetails.Count();

                Assert.That(afterCount - 1, Is.EqualTo(beforeCount));
            }
        }

        [Test]
        [Category("OrderDetails")]
        [Category("Happy")]
        public void CreateOrderDetail_OrderDetails_QueryReturnCorrectDetails()
        {
            _name = "Test Hunter";
            _location = "Debug Field";
            _date = DateTime.Now;

            using (var db = new MonsterHunterContext())
            {
                //Create Hunter
                db.Hunters.Add(new Hunter() { Name = _name, Location = _location });
                db.SaveChanges();
                var newHunterID = db.Hunters.Where(p => p.Name == "Test Hunter").Select(i => i.HunterId).FirstOrDefault();

                //Create Order
                db.Orders.Add(new Order() { HunterId = newHunterID, OrderDate = _date });
                db.SaveChanges();
                var newOrderID = db.Orders.Where(o => o.HunterId == newHunterID).Select(i => i.OrderId).FirstOrDefault();

                //Create Order Detail
                var productDetails = db.Products.Where(p => p.ProductName == "Potion").Select(o => o).FirstOrDefault();
               
                db.OrderDetails.Add(new OrderDetail() { OrderId = newOrderID, ProductId = productDetails.ProductId, Quantity = 10, UnitPrice = productDetails.UnitPrice });
                db.SaveChanges();

                var query = db.OrderDetails.Where(i => i.OrderId == newOrderID).FirstOrDefault();

                Assert.That(query.OrderId, Is.EqualTo(newOrderID));
                Assert.That(query.ProductId, Is.EqualTo(productDetails.ProductId));
                Assert.That(query.Quantity, Is.EqualTo(10));
                Assert.That(query.UnitPrice, Is.EqualTo(productDetails.UnitPrice));
            }
        }

        [Test]
        [Category("OrderDetails")]
        [Category("Happy")]
        public void DeleteOrderDetail_OrderDetails_CountDecreasesByOne()
        {
            _name = "Test Hunter";
            _location = "Debug Field";
            _date = DateTime.Now;

            using (var db = new MonsterHunterContext())
            {
                //Create Hunter
                db.Hunters.Add(new Hunter() { Name = _name, Location = _location });
                db.SaveChanges();
                var newHunterID = db.Hunters.Where(p => p.Name == "Test Hunter").Select(i => i.HunterId).FirstOrDefault();

                //Create Order
                db.Orders.Add(new Order() { HunterId = newHunterID, OrderDate = _date });
                db.SaveChanges();
                var newOrderID = db.Orders.Where(o => o.HunterId == newHunterID).Select(i => i.OrderId).FirstOrDefault();

                //Create Order Detail
                var productDetails = db.Products.Where(p => p.ProductName == "Potion").Select(o => o).FirstOrDefault();
                
                db.OrderDetails.Add(new OrderDetail() { OrderId = newOrderID, ProductId = productDetails.ProductId, Quantity = 10, UnitPrice = productDetails.UnitPrice });
                db.SaveChanges();

                var beforeCount = db.OrderDetails.Count();

                var query = db.OrderDetails.Where(i => i.ProductId == productDetails.ProductId).FirstOrDefault();
                db.OrderDetails.Remove(query);
                db.SaveChanges();

                var afterCount = db.OrderDetails.Count();

                Assert.That(afterCount + 1, Is.EqualTo(beforeCount));
            }
        }

        [Test]
        [Category("OrderDetails")]
        [Category("Sad")]
        public void DeleteOrderDetail_OrderDetails_QueryReturnNull()
        {
            _name = "Test Hunter";
            _location = "Debug Field";
            _date = DateTime.Now;

            using (var db = new MonsterHunterContext())
            {
                //Create Hunter
                db.Hunters.Add(new Hunter() { Name = _name, Location = _location });
                db.SaveChanges();
                var newHunterID = db.Hunters.Where(p => p.Name == "Test Hunter").Select(i => i.HunterId).FirstOrDefault();

                //Create Order
                db.Orders.Add(new Order() { HunterId = newHunterID, OrderDate = _date });
                db.SaveChanges();
                var newOrderID = db.Orders.Where(o => o.HunterId == newHunterID).Select(i => i.OrderId).FirstOrDefault();

                //Create Order Detail
                var productDetails = db.Products.Where(p => p.ProductName == "Potion").Select(o => o).FirstOrDefault();

                db.OrderDetails.Add(new OrderDetail() { OrderId = newOrderID, ProductId = productDetails.ProductId, Quantity = 10, UnitPrice = productDetails.UnitPrice });
                db.SaveChanges();

                var query = db.OrderDetails.Where(i => i.OrderId == newOrderID).FirstOrDefault();

                db.OrderDetails.Remove(query);
                db.SaveChanges();

                query = db.OrderDetails.Where(i => i.ProductId == productDetails.ProductId).FirstOrDefault();

                Assert.That(query, Is.Null);
            }
        }

        [TearDown]
        public void TearDown()
        {
            using (var db = new MonsterHunterContext())
            {
                //Finding and removing test input for each test
                var testHunter = db.Hunters.Where(h => h.Name == "Test Hunter");
                
                var testHunterId = 0;                
                testHunter.ToList().ForEach(i => testHunterId = i.HunterId);

                var testOrder = db.Orders.Where(h => h.HunterId == testHunterId);

                var testOrderDetailsID = db.OrderDetails.Include(p => p.Product).Where(p => p.Product.ProductName == "Potion").Select(i => i.OrderDetailsId).FirstOrDefault();
                var testOrderDetail = db.OrderDetails.Where(n => n.OrderDetailsId == testOrderDetailsID);
                var testProduct = db.Products.Where(p => p.ProductName == "Test Product");

                db.OrderDetails.RemoveRange(testOrderDetail);
                db.Orders.RemoveRange(testOrder);
                db.Hunters.RemoveRange(testHunter);
                db.Products.RemoveRange(testProduct);                
                db.SaveChanges();
            }
        }
    }
}
