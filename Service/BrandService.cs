using pract15_trpo.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pract15_trpo.Service
{
    public class BrandService
    {
        private readonly KaramovElectronicShopDbContext _db = DBService.Instance.Context;
        public static ObservableCollection<Brand> Brands { get; set; } = new();
        public BrandService()
        {
            GetAll();
        }
        public void Add(Brand brand)
        {
            var _brand = new Brand
            {
                Name = brand.Name,
            };
            _db.Brands.Add(_brand);
            Commit();
            Brands.Add(_brand);
        }
        public int Commit() => _db.SaveChanges();
        public void GetAll()
        {
            var brands = _db.Brands.ToList();

            Brands.Clear();
            foreach (var b in brands)
            {
                Brands.Add(b);
            }
        }
        public void Remove(Brand brand)
        {
            _db.Remove<Brand>(brand);
            if (Commit() > 0)
            {
                if (Brands.Contains(brand))
                {
                    Brands.Remove(brand);
                }
            }
        }
    }
}
