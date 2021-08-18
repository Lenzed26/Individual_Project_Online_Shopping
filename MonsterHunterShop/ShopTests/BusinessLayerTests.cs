using NUnit.Framework;
using ShopBusiness;
using ShopData;
using System.Linq;
using System.Data;
using System;
using Microsoft.EntityFrameworkCore;

namespace ShopTests
{
    class BusinessLayerTests
    {
        /// <summary>
        /// Unit tests using business manager classes to check for their functionality
        /// </summary>

        static string _name, _location, _category;
        static int _rarity;
        static decimal _price;
        static DateTime _date;

        HunterManager _hm = new HunterManager();
        OrderManager _om = new OrderManager();
        OrderDetailsManager _odm = new OrderDetailsManager();
        ProductManager _pm = new ProductManager();

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
        [Category("Hunter")]
        [Category("Happy")]
        public void HunterManagerCreate_Count_IncreasesByOne()
        {
            _name = "Test Hunter";
            _location = "Debug Field";
            var beforeCount = _hm.RetrieveAllHunters().Count();
            _hm.Create(_name, _location);
            var afterCount = _hm.RetrieveAllHunters().Count();

            Assert.That(afterCount - 1, Is.EqualTo(beforeCount));
        }

        [Test]
        [Category("Hunter")]
        [Category("Happy")]
        public void HunterManagerRetrieve_Returns_CorrectHunter()
        {
            _name = "Test Hunter";
            _location = "Debug Field";
            _hm.Create(_name, _location);
            var hunters = _hm.RetrieveAllHunters().Where(i => i.Name == _name).Select(i => i).FirstOrDefault();
            

            Assert.That(hunters.Name, Is.EqualTo(_name));
            Assert.That(hunters.Location, Is.EqualTo(_location));
        }

        [Test]
        [Category("Sad")]
        [Category("Hunter")]
        public void HunterManagerCreate_Duplicate_ThrowsException()
        {
            _name = "Test Hunter";
            _location = "Debug Field";
            _hm.Create(_name, _location);

            Assert.That(() => _hm.Create(_name, _location), Throws.TypeOf<ArgumentException>().With.Message.Contains("User already exists"));
        }

        [Test]
        [Category("Happy")]
        [Category("Hunter")]
        public void HunterManagerDeletes_Count_DecreasesByOne()
        {
            _name = "Test Hunter";
            _location = "Debug Field";
            _hm.Create(_name, _location);

            var beforeCount = _hm.RetrieveAllHunters().Count();
            _hm.Delete(_name);
            var afterCount = _hm.RetrieveAllHunters().Count();

            Assert.That(afterCount + 1, Is.EqualTo(beforeCount));
        }

        [Test]
        [Category("Sad")]
        [Category("Hunter")]
        public void HunterManagerDeletes_Retrieve_ReturnsNull()
        {
            _name = "Test Hunter";
            _location = "Debug Field";
            _hm.Create(_name, _location);
            _hm.Delete(_name);

            var hunters = _hm.RetrieveAllHunters().Where(n => n.Name == _name).Select(o=>o).FirstOrDefault();

            Assert.That(hunters, Is.Null);
        }

        [Test]
        [Category("Happy")]
        [Category("Hunter")]
        public void HunterManagerUpdates_Returns_True()
        {
            _name = "Test Hunter";
            _location = "Debug Field";
            _hm.Create(_name, _location);

            Assert.That(_hm.Update(_name, "Bherna Village"), Is.EqualTo(true));
        }

        [Test]
        [Category("Happy")]
        [Category("Hunter")]
        public void HunterManagerUpdates_Returns_CorrectDetails()
        {
            _name = "Test Hunter";
            _location = "Debug Field";
            _hm.Create(_name, _location);
            _hm.Update(_name, "Bherna Village");

            var hunters = _hm.RetrieveAllHunters().Where(n => n.Name == _name).FirstOrDefault();
            Assert.That(hunters.Name, Is.EqualTo(_name));
            Assert.That(hunters.Location, Is.EqualTo("Bherna Village"));
        }

        [Test]
        [Category("Sad")]
        [Category("Hunter")]
        public void HunterManagerUpdatesNonExistentHunter_Returns_False()
        {
            Assert.That(_hm.Update("Some Guy", "Test Hunter", "Debug Field"), Is.EqualTo(false));
        }

        [Test]
        [Category("Happy")]
        [Category("Product")]
        public void ProductManagerCreate_Count_IncreasesByOne()
        {
            _name = "Test Product";
            _category = "Test";
            _rarity = 99;
            _price = 101;

            var beforeCount = _pm.RetrieveAllProducts().Count();
            _pm.Create(_name, _category, _rarity, _price);
            var afterCount = _pm.RetrieveAllProducts().Count();

            Assert.That(afterCount - 1, Is.EqualTo(beforeCount));
        }

        [Test]
        [Category("Happy")]
        [Category("Product")]
        public void ProductManager_Returns_CorrectProduct()
        {
            _name = "Test Product";
            _category = "Test";
            _rarity = 99;
            _price = 101;
            _pm.Create(_name, _category, _rarity, _price);

            var products = _pm.RetrieveAllProducts().Where(p => p.ProductName == _name).FirstOrDefault();

            Assert.That(products.ProductName, Is.EqualTo(_name));
            Assert.That(products.Category, Is.EqualTo(_category));
            Assert.That(products.Rarity, Is.EqualTo(_rarity));
            Assert.That(products.UnitPrice, Is.EqualTo(_price));
            Assert.That(products.Description, Is.Null);
        }

        [Test]
        [Category("Sad")]
        [Category("Product")]
        public void ProductManagerCreates_Duplicate_ThrowsException()
        {
            _name = "Test Product";
            _category = "Test";
            _rarity = 99;
            _price = 101;
            _pm.Create(_name, _category, _rarity, _price);

            Assert.That(() => _pm.Create(_name, _category, _rarity, _price), Throws.TypeOf<ArgumentException>().With.Message.Contains("Item already exists"));
        }

        [Test]
        [Category("Happy")]
        [Category("Product")]
        public void ProductMangaerDeletesProduct_Count_DecreasesByOne()
        {
            _name = "Test Product";
            _category = "Test";
            _rarity = 99;
            _price = 101;
            _pm.Create(_name, _category, _rarity, _price);
            var beforeCount = _pm.RetrieveAllProducts().Count();

            _pm.Delete(_name);
            var afterCoount = _pm.RetrieveAllProducts().Count();

            Assert.That(afterCoount + 1, Is.EqualTo(beforeCount));
        }

        [Test]
        [Category("Sad")]
        [Category("Product")]
        public void ProductManagerDeletes_Retrieves_ReturnNull()
        {
            _name = "Test Product";
            _category = "Test";
            _rarity = 99;
            _price = 101;
            _pm.Create(_name, _category, _rarity, _price);
            _pm.Delete(_name);

            var products = _pm.RetrieveAllProducts().Where(p => p.ProductName == _name).FirstOrDefault();

            Assert.That(products, Is.Null);
        }

        [Test]
        [Category("Happy")]
        [Category("Product")]
        public void ProdcutManagerUpdate_Returns_True()
        {
            _name = "Test Product";
            _category = "Test";
            _rarity = 99;
            _price = 101;
            _pm.Create(_name, _category, _rarity, _price);

            Assert.That(_pm.Update(_name, _category, _rarity - 1, _price + 20, "Update Test"), Is.EqualTo(true));
        }

        [Test]
        [Category("Happy")]
        [Category("Product")]
        public void ProdcutManagerUpdate_Returns_CorrectDetails()
        {
            _name = "Test Product";
            _category = "Test";
            _rarity = 99;
            _price = 101;
            _pm.Create(_name, _category, _rarity, _price);
            _pm.Update(_name, _category, _rarity - 1, _price + 20, "Update Test");

            var products = _pm.RetrieveAllProducts().Where(p => p.ProductName == _name).FirstOrDefault();

            Assert.That(products.ProductName, Is.EqualTo(_name));
            Assert.That(products.Category, Is.EqualTo(_category));
            Assert.That(products.Rarity, Is.EqualTo(_rarity - 1));
            Assert.That(products.UnitPrice, Is.EqualTo(_price + 20));
            Assert.That(products.Description, Is.EqualTo("Update Test"));
        }

        [Test]
        [Category("Sad")]
        [Category("Product")]
        public void ProductManagerUpdatesNonExistentHunter_Returns_False()
        {
            Assert.That(_pm.Update("NonExistent", "nope", 0, 0), Is.EqualTo(false));
        }

        [Test]
        [Category("Happy")]
        [Category("Order")]
        public void OrderManagerCreate_Count_IncreasesByOne()
        {
            _name = "Test Hunter";
            _location = "Debug Field";
            _hm.Create(_name, _location);

            var hunterId = _hm.RetrieveAllHunters().Where(n => n.Name == _name).Select(i => i.HunterId).FirstOrDefault();

            _date = DateTime.Now;
            var beforeCount = _om.RetrieveAllOrders().Count();
            _om.Create(hunterId, _date);
            var afterCount = _om.RetrieveAllOrders().Count();

            Assert.That(afterCount - 1, Is.EqualTo(beforeCount));
        }

        [Test]
        [Category("Happy")]
        [Category("Order")]
        public void OrderManagerRetrieve_Returns_CorrectOrder()
        {
            _name = "Test Hunter";
            _location = "Debug Field";
            _hm.Create(_name, _location);
            var hunterId = _hm.RetrieveAllHunters().Where(n => n.Name == _name).Select(i => i.HunterId).FirstOrDefault();
            _date = DateTime.Now.Date;
            _om.Create(hunterId, _date);
            var orders = _om.RetrieveAllOrders().Where(o => o.HunterId == hunterId).FirstOrDefault();

            Assert.That(orders.HunterId, Is.EqualTo(hunterId));
            Assert.That(orders.OrderDate, Is.EqualTo(_date));
            Assert.That(orders.DeliveryDate, Is.Null);
        }

        [Test]
        [Category("Happy")]
        [Category("Order")]
        public void OrderManagerDelete_Count_DecreasesByOne()
        {
            _name = "Test Hunter";
            _location = "Debug Field";
            _hm.Create(_name, _location);

            var hunterId = _hm.RetrieveAllHunters().Where(n => n.Name == _name).Select(i => i.HunterId).FirstOrDefault();

            _date = DateTime.Now.Date;            
            _om.Create(hunterId, _date);
            var beforeCount = _om.RetrieveAllOrders().Count();
            var query = _om.RetrieveAllOrders().Where(h => h.HunterId == hunterId).Select(o => o.OrderId).FirstOrDefault();
            _om.Delete(query);
            var afterCount = _om.RetrieveAllOrders().Count();

            Assert.That(afterCount + 1, Is.EqualTo(beforeCount));
        }

        [Test]
        [Category("Sad")]
        [Category("Order")]
        public void OrderManagerDeletes_Retrieve_ReturnsNull()
        {
            _name = "Test Hunter";
            _location = "Debug Field";
            _hm.Create(_name, _location);

            var hunterId = _hm.RetrieveAllHunters().Where(n => n.Name == _name).Select(i => i.HunterId).FirstOrDefault();

            _date = DateTime.Now.Date;
            _om.Create(hunterId, _date);
            var query = _om.RetrieveAllOrders().Where(h => h.HunterId == hunterId).Select(o => o.OrderId).FirstOrDefault();
            _om.Delete(query);
            
            var orders = _om.RetrieveAllOrders().Where(h => h.HunterId == hunterId).FirstOrDefault();
            Assert.That(orders, Is.Null);
        }

        [Test]
        [Category("Happy")]
        [Category("Order")]
        public void OrderManagerUpdate_Returns_True()
        {
            _name = "Test Hunter";
            _location = "Debug Field";
            _hm.Create(_name, _location);

            var hunterId = _hm.RetrieveAllHunters().Where(n => n.Name == _name).Select(i => i.HunterId).FirstOrDefault();

            _date = DateTime.Now.Date;
            _om.Create(hunterId, _date);

            var orderId = _om.RetrieveAllOrders().Where(h => h.HunterId == hunterId).Select(o => o.OrderId).FirstOrDefault();

            var deliverDate = DateTime.Now.Date;
            Assert.That(_om.Update(orderId, deliverDate), Is.True);
        }

        [Test]
        [Category("Happy")]
        [Category("Order")]
        public void OrderManagerUpdate_Returns_CorrectDetails()
        {
            _name = "Test Hunter";
            _location = "Debug Field";
            _hm.Create(_name, _location);

            var hunterId = _hm.RetrieveAllHunters().Where(n => n.Name == _name).Select(i => i.HunterId).FirstOrDefault();

            _date = DateTime.Now.Date;
            _om.Create(hunterId, _date);

            var orderId = _om.RetrieveAllOrders().Where(h => h.HunterId == hunterId).Select(o => o.OrderId).FirstOrDefault();

            var deliverDate = DateTime.Now.Date;
            _om.Update(orderId, deliverDate);

            var orders = _om.RetrieveAllOrders().Where(h => h.HunterId == hunterId).FirstOrDefault();
            Assert.That(orders.OrderId, Is.EqualTo(orderId));
            Assert.That(orders.OrderDate, Is.EqualTo(_date));
            Assert.That(orders.DeliveryDate, Is.EqualTo(deliverDate));
        }

        [Test]
        [Category("Sad")]
        [Category("Order")]
        public void OrderManagerUpdateNonExistent_Returns_False()
        {
            Assert.That(_om.Update(9999, DateTime.Now.Date), Is.False);
        }

        [Test]
        [Category("Happy")]
        [Category("OrderDetail")]
        public void OrderDetailManagerCreate_Count_IncreasesByOne()
        {
            _name = "Test Hunter";
            _location = "Debug Field";
            _date = DateTime.Now.Date;

            //Create Hunter
            _hm.Create(_name, _location);

            //Create Order
            var hunterId = _hm.RetrieveAllHunters().Where(n => n.Name == _name).Select(i => i.HunterId).FirstOrDefault();
            _om.Create(hunterId, _date);

            //Create Order Detail
            var orderId = _om.RetrieveAllOrders().Where(o => o.HunterId == hunterId).Select(i => i.OrderId).FirstOrDefault();
            var product = _pm.RetrieveAllProducts().Where(n => n.ProductName == "Potion").FirstOrDefault();

            var beforeCount = _odm.RetrieveAllOrderDetails().Count();
            _odm.Create(orderId, product.ProductId, 10, product.UnitPrice);
            var afterCount = _odm.RetrieveAllOrderDetails().Count();

            Assert.That(afterCount - 1, Is.EqualTo(beforeCount));
        }

        [Test]
        [Category("OrderDetail")]
        [Category("Happy")]
        public void OrderDetailManagerRetrieve_Returns_CorrectOrderDetail()
        {
            _name = "Test Hunter";
            _location = "Debug Field";
            _date = DateTime.Now.Date;

            //Create Hunter
            _hm.Create(_name, _location);

            //Create Order
            var hunterId = _hm.RetrieveAllHunters().Where(n => n.Name == _name).Select(i => i.HunterId).FirstOrDefault();
            _om.Create(hunterId, _date);

            //Create Order Detail
            var orderId = _om.RetrieveAllOrders().Where(o => o.HunterId == hunterId).Select(i => i.OrderId).FirstOrDefault();
            var product = _pm.RetrieveAllProducts().Where(n => n.ProductName == "Potion").FirstOrDefault();
            
            _odm.Create(orderId, product.ProductId, 10, product.UnitPrice);

            var query = _odm.RetrieveAllOrderDetails().Where(i => i.OrderId == orderId).FirstOrDefault();

            Assert.AreEqual(query.OrderId, orderId);
            Assert.AreEqual(query.ProductId, product.ProductId);
            Assert.AreEqual(query.Quantity, 10);
            Assert.AreEqual(query.UnitPrice, product.UnitPrice);
        }

        [Test]
        [Category("Happy")]
        [Category("OrderDetail")]
        public void OrderDetailManagerDelete_Count_DecreasesByOne()
        {
            _name = "Test Hunter";
            _location = "Debug Field";
            _date = DateTime.Now.Date;

            //Create Hunter
            _hm.Create(_name, _location);

            //Create Order
            var hunterId = _hm.RetrieveAllHunters().Where(n => n.Name == _name).Select(i => i.HunterId).FirstOrDefault();
            _om.Create(hunterId, _date);

            //Create Order Detail
            var orderId = _om.RetrieveAllOrders().Where(o => o.HunterId == hunterId).Select(i => i.OrderId).FirstOrDefault();
            var product = _pm.RetrieveAllProducts().Where(n => n.ProductName == "Potion").FirstOrDefault();

            
            _odm.Create(orderId, product.ProductId, 10, product.UnitPrice);
            var orderDetailId = _odm.RetrieveAllOrderDetails().Where(o => o.OrderId == orderId && o.ProductId == product.ProductId).Select(i => i.OrderDetailsId).FirstOrDefault();

            var beforeCount = _odm.RetrieveAllOrderDetails().Count();
            _odm.Delete(orderDetailId);
            var afterCount = _odm.RetrieveAllOrderDetails().Count();

            Assert.That(afterCount + 1, Is.EqualTo(beforeCount));
        }

        [Test]
        [Category("Sad")]
        [Category("OrderDetail")]
        public void OrderDetailManagerDeletes_Retrieve_ReturnsNull()
        {
            _name = "Test Hunter";
            _location = "Debug Field";
            _date = DateTime.Now.Date;

            //Create Hunter
            _hm.Create(_name, _location);

            //Create Order
            var hunterId = _hm.RetrieveAllHunters().Where(n => n.Name == _name).Select(i => i.HunterId).FirstOrDefault();
            _om.Create(hunterId, _date);

            //Create Order Detail
            var orderId = _om.RetrieveAllOrders().Where(o => o.HunterId == hunterId).Select(i => i.OrderId).FirstOrDefault();
            var product = _pm.RetrieveAllProducts().Where(n => n.ProductName == "Potion").FirstOrDefault();


            _odm.Create(orderId, product.ProductId, 10, product.UnitPrice);
            var orderDetailId = _odm.RetrieveAllOrderDetails().Where(o => o.OrderId == orderId && o.ProductId == product.ProductId).Select(i => i.OrderDetailsId).FirstOrDefault();

            _odm.Delete(orderDetailId);
        }

        [Test]
        [Category("Happy")]
        [Category("OrderDetail")]
        public void OrderDetailManagerUpdate_Returns_True()
        {
            _name = "Test Hunter";
            _location = "Debug Field";
            _date = DateTime.Now.Date;

            //Create Hunter
            _hm.Create(_name, _location);

            //Create Order
            var hunterId = _hm.RetrieveAllHunters().Where(n => n.Name == _name).Select(i => i.HunterId).FirstOrDefault();
            _om.Create(hunterId, _date);

            //Create Order Detail
            var orderId = _om.RetrieveAllOrders().Where(o => o.HunterId == hunterId).Select(i => i.OrderId).FirstOrDefault();
            var product = _pm.RetrieveAllProducts().Where(n => n.ProductName == "Potion").FirstOrDefault();


            _odm.Create(orderId, product.ProductId, 10, product.UnitPrice);

            Assert.IsTrue(_odm.Update(orderId, product.ProductId, 20));
        }

        [Test]
        [Category("Happy")]
        [Category("OrderDetail")]
        public void OrderDetailManagerUpdate_Returns_CorrectDetails()
        {
            _name = "Test Hunter";
            _location = "Debug Field";
            _date = DateTime.Now.Date;

            //Create Hunter
            _hm.Create(_name, _location);

            //Create Order
            var hunterId = _hm.RetrieveAllHunters().Where(n => n.Name == _name).Select(i => i.HunterId).FirstOrDefault();
            _om.Create(hunterId, _date);

            //Create Order Detail
            var orderId = _om.RetrieveAllOrders().Where(o => o.HunterId == hunterId).Select(i => i.OrderId).FirstOrDefault();
            var product = _pm.RetrieveAllProducts().Where(n => n.ProductName == "Potion").FirstOrDefault();


            _odm.Create(orderId, product.ProductId, 10, product.UnitPrice);
            _odm.Update(orderId, product.ProductId, 20);

            var orderDetail = _odm.RetrieveAllOrderDetails().Where(o => o.OrderId == orderId && o.ProductId == product.ProductId).FirstOrDefault();

            Assert.AreEqual(orderDetail.Quantity, 20);
        }

        [Test]
        [Category("Sad")]
        [Category("OrderDetail")]
        public void OrderDetailManagerUpdateNonExistent_Returns_False()
        {
            Assert.IsFalse(_odm.Update(25, 25, 25));
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
