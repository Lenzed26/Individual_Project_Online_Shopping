using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ShopData;

namespace ShopBusiness
{
    public class HunterManager
    {
        public Hunter SelectedHunter { get; set; }

        public List<Hunter> RetrieveAllHunters()
        {
            using (var db = new MonsterHunterContext())
            {
                return db.Hunters.ToList();
            }
        }

        public void SetSelectedHunter(object selectedItem)
        {
            SelectedHunter = (Hunter)selectedItem;
        }

        public void Create(string name, string location)
        {
            var newHunter = new Hunter() { Name = name, Location = location };
            using (var db = new MonsterHunterContext())
            {
                if (CheckDuplicate(name) == false)
                {
                    db.Hunters.Add(newHunter);
                    db.SaveChanges();
                }
                else
                {
                    throw new ArgumentException("User already exists");
                }
            }
        }

        public bool Update(string name, string location)
        {
            using (var db = new MonsterHunterContext())
            {                
                if(CheckDuplicate(name) == true)
                {
                    return false;
                }
                SelectedHunter.Location = location;
                db.SaveChanges();
            }
            return true;
        }

        public bool Update(string oldName, string newName, string location)
        {
            using (var db = new MonsterHunterContext())
            {                
                if (CheckDuplicate(newName) == false)
                {
                    SelectedHunter.Name = newName;
                    SelectedHunter.Location = location;
                    db.SaveChanges();
                }
                else
                {
                    throw new ArgumentException($"User with name {newName} already exists");
                }
            }
            return true;
        }

        public bool Delete(string name)
        {
            using (var db = new MonsterHunterContext())
            {
                SelectedHunter = db.Hunters.Where(u => u.Name.Equals(name)).FirstOrDefault();
                if(SelectedHunter == null)
                {
                    return false;
                }

                db.Remove(SelectedHunter);

                var RemoveFromOrders = db.Orders.Include(h => h.Hunter).Where(n => n.Hunter.Name.Equals(name)).FirstOrDefault();
                if(RemoveFromOrders != null) db.Remove(RemoveFromOrders);

                var RemoveFromOrderDetails = db.OrderDetails.Include(o => o.Order).ThenInclude(h => h.Hunter).Where(n => n.Order.Hunter.Name.Equals(name)).FirstOrDefault();
                if (RemoveFromOrderDetails != null) db.Remove(RemoveFromOrderDetails);

                db.SaveChanges();
            }
            return true;
        }

        public bool CheckDuplicate(string name)
        {
            using (var db = new MonsterHunterContext())
            {
                var query = db.Hunters.Where(u => u.Name.Equals(name)).FirstOrDefault();
                if(query == null)
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
