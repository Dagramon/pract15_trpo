using Microsoft.EntityFrameworkCore;
using pract15_trpo.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace pract15_trpo.Service
{
    public class ProductService
    {
        private readonly KaramovElectronicShopDbContext _db = DBService.Instance.Context;
        public static ObservableCollection<Product> Products { get; set; } = new();
        public ProductService()
        {
            GetAll();
        }
        public void Add(Product product)
        {
            var _product = new Product
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Rating = product.Rating,
                Stock = product.Stock,
                Brand = product.Brand,
                BrandId = product.BrandId,
                Category = product.Category,
                CategoryId = product.CategoryId,
                CreatedAt = DateTime.Now,
                Tags = product.Tags,
            };
            _db.Products.Add(_product);
            Commit();
            Products.Add(_product);
        }
        public int Commit() => _db.SaveChanges();
        public void GetAll()
        {
            var products = _db.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Include(p => p.Tags)
                .ToList();

            Products.Clear();
            foreach (var p in products)
            {
                Products.Add(p);
            }
        }
        public void Remove(Product product)
        {
            _db.Remove<Product>(product);
            if (Commit() > 0)
            {
                if (Products.Contains(product))
                {
                    Products.Remove(product);
                }
            }
        }
    }
}
