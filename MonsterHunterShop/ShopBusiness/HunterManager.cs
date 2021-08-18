using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ShopData;
using ShopData.Services;

namespace ShopBusiness
{
    public class HunterManager
    {
        private IHunterService _service;

        public HunterManager(IHunterService service)
        {
            if (service == null)
            {
                throw new ArgumentException("IHunterService object cannot be null");
            }
            _service = service;
        }

        public HunterManager()
        {
            _service = new HunterService();
        }

        public Hunter SelectedHunter { get; set; }

        public List<Hunter> RetrieveAllHunters()
        {
            return _service.GetHunterList();
        }

        public void SetSelectedHunter(object selectedItem)
        {
            SelectedHunter = (Hunter)selectedItem;
        }

        public void Create(string name, string location)
        {
            var newHunter = new Hunter() { Name = name, Location = location };
            
            if (CheckDuplicate(name) == true)
            {
                _service.CreateHunter(newHunter);
            }
            else
            {
                throw new ArgumentException("User already exists");
            }
        }

        public bool Update(string name, string location)
        {
            var customer = _service.GetHunterByName(name);     
            
            if(customer == null)
            {
                return false;
            }

            customer.Location = location;            

            try
            {
                _service.SaveHunterChanges();
                SelectedHunter = customer;
            }
            catch (Exception e)
            {
                return false;
            }            
            return true;
        }

        public bool Update(string oldName, string newName, string location)
        {
            var customer = _service.GetHunterByName(oldName);
            
            if (CheckDuplicate(newName) == false || customer == null)
            {
                return false;
            }

            customer.Name = newName;
            customer.Location = location;
            _service.SaveHunterChanges(); 
            
            return true;
        }

        public bool Delete(string name)
        {            
            SelectedHunter = _service.GetHunterByName(name);
            if(SelectedHunter == null)
            {
                return false;
            }
            _service.RemoveHunter(SelectedHunter);
            _service.SaveHunterChanges();
            return true;
        }

        public bool CheckDuplicate(string name)
        {            
            var query = _service.GetHunterByName(name);
            if(query == null)
            {
                return true;
            }
            else
            {
                return false;
            }            
        }
    }
}
