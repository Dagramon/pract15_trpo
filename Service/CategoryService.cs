using pract15_trpo.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pract15_trpo.Service
{
    public class CategoryService
    {
        private readonly KaramovElectronicShopDbContext _db = DBService.Instance.Context;
        public static ObservableCollection<Category> Categories { get; set; } = new();
        public CategoryService()
        {
            GetAll();
        }
        public void Add(Category category)
        {
            var _category = new Category
            {
                Name = category.Name,
            };
            _db.Categories.Add(_category);
            Commit();
            Categories.Add(_category);
        }
        public int Commit() => _db.SaveChanges();
        public void GetAll()
        {
            var categories = _db.Categories.ToList();

            Categories.Clear();
            foreach (var c in categories)
            {
                Categories.Add(c);
            }
        }
        public void Remove(Category category)
        {
            _db.Remove<Category>(category);
            if (Commit() > 0)
            {
                if (Categories.Contains(category))
                {
                    Categories.Remove(category);
                }
            }
        }
    }
}
