using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopData.Services
{    
    public class HunterService :IHunterService
    {
        private readonly MonsterHunterContext _context;

        public HunterService(MonsterHunterContext context)
        {
            _context = context;
        }

        public HunterService()
        {
            _context = new MonsterHunterContext();
        }

        public void CreateHunter(Hunter h)
        {
            _context.Hunters.Add(h);
            _context.SaveChanges();
        }

        public Hunter GetHunterByName(string hunterName)
        {
            return _context.Hunters.Where(h=>h.Name == hunterName).FirstOrDefault();
        }

        public List<Hunter> GetHunterList()
        {
            return _context.Hunters.ToList();
        }

        public void RemoveHunter(Hunter h)
        {
            var od = _context.OrderDetails.Include(o => o.Order).ThenInclude(h => h.Hunter).Where(n => n.Order.Hunter.Name.Equals(h.Name)).FirstOrDefault();
            var o = _context.Orders.Include(h => h.Hunter).Where(n => n.Hunter.Name.Equals(h.Name)).FirstOrDefault();
            if (od != null) _context.Remove(o);
            if (o != null) _context.Remove(od);            
            _context.Hunters.Remove(h);
            _context.SaveChanges();
        }

        public void SaveHunterChanges()
        {
            _context.SaveChanges();
        }
    }
}
