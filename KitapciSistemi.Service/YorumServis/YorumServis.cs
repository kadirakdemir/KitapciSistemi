using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using KitapciSistemi.Core.DataBase.Entities;
using KitapciSistemi.Data.DataContext;
using System.Data.Entity.Migrations;

namespace KitapciSistemi.Service.YorumServis
{
    public class YorumServis : IYorumServis
    {
        private readonly KitapciContext _context = new KitapciContext();
        public int Count()
        {
            return _context.Yorum.Count();
        }

        public void Delete(int id)
        {
            var Yorum = GetById(id);
            if (Yorum != null)
            {
                _context.Yorum.Remove(Yorum);
            }
        }

        public Yorum Get(Expression<Func<Yorum, bool>> expression)
        {
            return _context.Yorum.FirstOrDefault(expression);
        }

        public IEnumerable<Yorum> GetAll()
        {
            return _context.Yorum.Select(x => x);
        }

        public Yorum GetById(int id)
        {
            return _context.Yorum.FirstOrDefault(x => x.ID == id);
        }

        public IQueryable<Yorum> GetMany(Expression<Func<Yorum, bool>> expression)
        {
            return _context.Yorum.Where(expression);
        }

        public void Insert(Yorum Entity)
        {
            _context.Yorum.Add(Entity);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Yorum Entity)
        {
            _context.Yorum.AddOrUpdate();
        }
    }
}
