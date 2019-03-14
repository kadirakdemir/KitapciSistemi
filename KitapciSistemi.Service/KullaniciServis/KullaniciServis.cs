using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using KitapciSistemi.Core.DataBase.Entities;
using KitapciSistemi.Data.DataContext;
using System.Data.Entity.Migrations;

namespace KitapciSistemi.Service.KullaniciServis
{
    public class KullaniciServis : IKullaniciServis
    {
        private readonly KitapciContext _context = new KitapciContext();
        public int Count()
        {
            return _context.Kullanici.Count();
        }

        public void Delete(int id)
        {
            var Kullanici = GetById(id);
            if (Kullanici!=null)
            {
                _context.Kullanici.Remove(Kullanici);
            }
        }

        public Kullanici Get(Expression<Func<Kullanici, bool>> expression)
        {
            return _context.Kullanici.FirstOrDefault(expression);
        }

        public IEnumerable<Kullanici> GetAll()
        {
            return _context.Kullanici.Select(x => x);
        }

        public Kullanici GetById(int id)
        {
            return _context.Kullanici.FirstOrDefault(x => x.ID == id);
        }

        public IQueryable<Kullanici> GetMany(Expression<Func<Kullanici, bool>> expression)
        {
            return _context.Kullanici.Where(expression);
        }

        public void Insert(Kullanici Entity)
        {
            _context.Kullanici.Add(Entity);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Kullanici Entity)
        {
            _context.Kullanici.AddOrUpdate();
        }
    }
}
