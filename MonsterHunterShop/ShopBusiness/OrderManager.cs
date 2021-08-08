using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ShopData;

namespace ShopBusiness
{
    public class OrderManager
    {
        public Order SelectedOrder { get; set; }

        public List<Order> RetrieveAllOrders()
        {
            using (var db = new MonsterHunterContext())
            {
                return db.Orders.ToList();
            }
        }

        public Order ConvertToOrder(DetailedOrder detailedOrder)
        {
            var hunterId = 0;
            using(var db = new MonsterHunterContext())
            {
                hunterId = db.Hunters.Where(i => i.Name == detailedOrder.HunterName).FirstOrDefault().HunterId;
            }
            return new Order() { OrderId = detailedOrder.OrderId ?? default(int), OrderDate = detailedOrder.OrderDate, DeliveryDate = detailedOrder.DeliveryDate, HunterId = hunterId };
        }

        public void SetSelectedOrder(object selectedItem)
        {
            SelectedOrder = (Order)selectedItem;
        }

        public void Create(int hunterId, DateTime orderDate, DateTime? deliveryDate = null)
        {
            var newOrder = new Order() { HunterId = hunterId, OrderDate = orderDate, DeliveryDate = deliveryDate };
            using (var db = new MonsterHunterContext())
            {
                db.Orders.Add(newOrder);
                db.SaveChanges();
            }
        }

        public bool Update(int orderId, DateTime deliveryDate)
        {
            using (var db = new MonsterHunterContext())
            {
                SelectedOrder = db.Orders.Where(o => o.OrderId == orderId).FirstOrDefault();
                if(SelectedOrder == null)
                {
                    return false;
                }
                if(deliveryDate < SelectedOrder.OrderDate)
                {
                    throw new ArgumentOutOfRangeException("Delivery date cannot be set before order date");
                }

                SelectedOrder.DeliveryDate = deliveryDate;

                db.SaveChanges();
            }
            return true;
        }

        public bool Update(int orderId, DateTime orderDate, DateTime deliveryDate)
        {
            using (var db = new MonsterHunterContext())
            {
                SelectedOrder = db.Orders.Where(o => o.OrderId == orderId).FirstOrDefault();
                if (SelectedOrder == null)
                {
                    return false;
                }
                if (deliveryDate < SelectedOrder.OrderDate || orderDate > deliveryDate)
                {
                    throw new ArgumentOutOfRangeException("Order and Delivery dates cannot pass each other");
                }
                SelectedOrder.OrderDate = orderDate;
                SelectedOrder.DeliveryDate = deliveryDate;

                db.SaveChanges();
            }
            return true;
        }

        public bool Delete(int orderId)
        {
            using (var db = new MonsterHunterContext())
            {
                SelectedOrder = db.Orders.Where(o => o.OrderId == orderId).FirstOrDefault();
                if(SelectedOrder == null)
                {
                    return false;
                }

                db.Orders.Remove(SelectedOrder);

                //var RemoveFromOrderDetails = db.OrderDetails.Include(o => o.Order).Where(i => i.Order.OrderId == orderId).FirstOrDefault();
                //if (RemoveFromOrderDetails != null) db.OrderDetails.Remove(RemoveFromOrderDetails);

                db.SaveChanges();
            }
            return true;
        }
    }
}
