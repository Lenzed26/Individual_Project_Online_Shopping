using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ShopData;

namespace ShopBusiness
{
    class OrderDetailsManager
    {
        public OrderDetail SelectedOrderDetail { get; set; }

        public List<OrderDetail> RetrieveAllOrderDetails()
        {
            using (var db = new MonsterHunterContext())
            {
                return db.OrderDetails.ToList();
            }
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
