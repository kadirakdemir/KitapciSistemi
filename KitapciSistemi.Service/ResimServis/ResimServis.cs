using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using KitapciSistemi.Core.DataBase.Entities;
using KitapciSistemi.Data.DataContext;
using System.Data.Entity.Migrations;

namespace KitapciSistemi.Service.ResimServis
{
    public class ResimServis : IResimServis
    {
        private readonly KitapciContext _context = new KitapciContext();
        public int Count()
        {
            return _context.Resim.Count();
        }

        public void Delete(int id)
        {
            var Resim = GetById(id);
            if (Resim != null)
            {
                _context.Resim.Remove(Resim);
            }
        }

        public Resim Get(Expression<Func<Resim, bool>> expression)
        {
            return _context.Resim.FirstOrDefault(expression);
        }

        public IEnumerable<Resim> GetAll()
        {
            return _context.Resim.Select(x => x);
        }

        public Resim GetById(int id)
        {
            return _context.Resim.FirstOrDefault(x => x.ID == id);
        }

        public IQueryable<Resim> GetMany(Expression<Func<Resim, bool>> expression)
        {
            return _context.Resim.Where(expression);
        }

        public void Insert(Resim Entity)
        {
            _context.Resim.Add(Entity);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Resim Entity)
        {
            _context.Resim.AddOrUpdate();
        }
    }
}
