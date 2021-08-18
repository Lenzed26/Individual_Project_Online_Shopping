using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopData.Services
{
    public interface IHunterService
    {
        public List<Hunter> GetHunterList();
        public Hunter GetHunterByName(string hunterName);
        public void CreateHunter(Hunter h);
        public void SaveHunterChanges();
        public void RemoveHunter(Hunter h);
    }
}
