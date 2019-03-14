using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using KitapciSistemi.Core.DataBase.Entities;
using KitapciSistemi.Data.DataContext;
using System.Data.Entity.Migrations;

namespace KitapciSistemi.Service.UrunServis
{
    public class UrunServis : IUrunServis
    {
        private readonly KitapciContext _context = new KitapciContext();
        public int Count()
        {
            return _context.Urun.Count();
        }

        public void Delete(int id)
        {
            var Urun = GetById(id);
            if (Urun != null)
            {
                _context.Urun.Remove(Urun);
            }
        }

        public Urun Get(Expression<Func<Urun, bool>> expression)
        {
            return _context.Urun.FirstOrDefault(expression);
        }

        public IEnumerable<Urun> GetAll()
        {
            return _context.Urun.Select(x => x);
        }

        public Urun GetById(int id)
        {
            return _context.Urun.FirstOrDefault(x => x.ID == id);
        }

        public IQueryable<Urun> GetMany(Expression<Func<Urun, bool>> expression)
        {
            return _context.Urun.Where(expression);
        }

        public void Insert(Urun Entity)
        {
            _context.Urun.Add(Entity);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Urun Entity)
        {
            _context.Urun.AddOrUpdate();
        }
    }
}
