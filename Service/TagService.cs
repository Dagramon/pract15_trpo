using Microsoft.EntityFrameworkCore;
using pract15_trpo.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pract15_trpo.Service
{
    public class TagService
    {
        private readonly KaramovElectronicShopDbContext _db = DBService.Instance.Context;
        public static ObservableCollection<Tag> Tags { get; set; } = new();
        public TagService()
        {
            GetAll();
        }
        public void Add(Tag tag)
        {
            var _tag = new Tag
            {
                Name = tag.Name,
            };
            _db.Tags.Add(_tag);
            Commit();
            Tags.Add(_tag);
        }
        public int Commit() => _db.SaveChanges();
        public void GetAll()
        {
            var tags = _db.Tags.ToList();

            Tags.Clear();
            foreach (var t in tags)
            {
                Tags.Add(t);
            }
        }
        public void Remove(Tag tag)
        {
            _db.Remove<Tag>(tag);
            if (Commit() > 0)
            {
                if (Tags.Contains(tag))
                {
                    Tags.Remove(tag);
                }
            }
        }
    }
}
