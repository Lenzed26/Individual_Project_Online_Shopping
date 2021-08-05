using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ShopData;

namespace ShopBusiness
{
    public class DetailedOrder
    {
        public int OrderDetailsId { get; set; }
        public int? OrderId { get; set; }
        public string HunterName { get; set; }
        public int? ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

    public class OrderDetailsManager
    {
        public OrderDetail SelectedOrderDetail { get; set; }

        public List<OrderDetail> RetrieveAllOrderDetails()
        {
            using (var db = new MonsterHunterContext())
            {
                return db.OrderDetails.ToList();
            }
        }

        public List<DetailedOrder> RetrieveAllDetailedOrders()
        {
            DetailedOrder detailedOrder = new DetailedOrder();
            List<DetailedOrder> returnList = new List<DetailedOrder>();
            using (var db = new MonsterHunterContext())
            {
                var query = db.OrderDetails.Include(o => o.Order).ThenInclude(h => h.Hunter).Include(p => p.Product).Select(o => new { OrderDetailsId = o.OrderDetailsId, OrderId = o.OrderId, ProductId = o.ProductId, HunterName = o.Order.Hunter.Name, ProductName = o.Product.ProductName, Quantity = o.Quantity, Price = o.UnitPrice });
                        //from od in db.OrderDetails
                        //join o in db.Orders on od.OrderId equals o.OrderId
                        //join h in db.Hunters on o.HunterId equals h.HunterId
                        //join p in db.Products on od.ProductId equals p.ProductId
                        //select new
                        //{
                        //    OrderDetailsId = od.OrderDetailsId,
                        //    OrderId = od.OrderId,
                        //    ProductId = od.ProductId,
                        //    HunterName = h.Name,
                        //    ProductName = p.ProductName,
                        //    Quantity = od.Quantity,
                        //    Price = od.UnitPrice
                        //};

                foreach (var item in query)
                {                    
                    detailedOrder.OrderDetailsId = item.OrderDetailsId;
                    detailedOrder.OrderId = item.OrderId;
                    detailedOrder.ProductId = item.ProductId;
                    detailedOrder.HunterName = item.HunterName;
                    detailedOrder.ProductName = item.ProductName;
                    detailedOrder.Quantity = item.Quantity;
                    detailedOrder.Price = item.Price;
                    returnList.Add(detailedOrder);
                }
            }
            return returnList;
        }


        public void SetSelectedOrderDetail(object selectedItem)
        {
            SelectedOrderDetail = (OrderDetail)selectedItem;
        }

        public void Create(int orderId, int productId, int quantity, decimal price)
        {
            var newOrderDetail = new OrderDetail() { OrderId = orderId, ProductId = productId, Quantity = quantity, UnitPrice = price };
            using (var db = new MonsterHunterContext())
            {
                db.OrderDetails.Add(newOrderDetail);
                db.SaveChanges();
            }
        }

        public bool Update(int orderId, int productId, int newQuantity)
        {
            using (var db = new MonsterHunterContext())
            {
                SelectedOrderDetail = db.OrderDetails.Where(o => o.OrderId == orderId && o.ProductId == productId).FirstOrDefault();
                if(SelectedOrderDetail == null)
                {
                    return false;
                }
                SelectedOrderDetail.Quantity = newQuantity;

                db.SaveChanges();
            }
            return true;
        }

        public bool Delete(int orderDetailId)
        {
            using (var db = new MonsterHunterContext())
            {
                SelectedOrderDetail = db.OrderDetails.Where(i => i.OrderDetailsId == orderDetailId).FirstOrDefault();
                if (SelectedOrderDetail == null)
                {
                    Debug.WriteLine($"OrderDetail with id {orderDetailId} is not found");
                    return false;
                }

                db.Remove(SelectedOrderDetail);
                db.SaveChanges();
            }
            return true;
        }
    }
}
