using pract15_trpo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pract15_trpo.Service
{
    public class DBService
    {
        private KaramovElectronicShopDbContext context;
        public KaramovElectronicShopDbContext Context => context;
        private static DBService? instance;
        public static DBService Instance
        {
            get
            {
                if (instance == null)
                    instance = new DBService();
                return instance;
            }
        }
        private DBService()
        {
            context = new KaramovElectronicShopDbContext();
        }
    }
}
