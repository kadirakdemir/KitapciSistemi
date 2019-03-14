using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using KitapciSistemi.Core.DataBase.Entities;
using KitapciSistemi.Data.DataContext;
using System.Data.Entity.Migrations;

namespace KitapciSistemi.Service.KategoriServis
{
    public class KategoriServis : IKategoriServis
    {
        private readonly KitapciContext _context = new KitapciContext();
        public int Count()
        {
            return _context.Kategori.Count();
        }

        public void Delete(int id)
        {
            var Kategori = GetById(id);
            if (Kategori != null)
            {
                _context.Kategori.Remove(Kategori);
            }
        }

        public Kategori Get(Expression<Func<Kategori, bool>> expression)
        {
            return _context.Kategori.FirstOrDefault(expression);
        }

        public IEnumerable<Kategori> GetAll()
        {
            return _context.Kategori.Select(x => x);
        }

        public Kategori GetById(int id)
        {
            return _context.Kategori.FirstOrDefault(x => x.ID == id);
        }

        public IQueryable<Kategori> GetMany(Expression<Func<Kategori, bool>> expression)
        {
            return _context.Kategori.Where(expression);
        }

        public void Insert(Kategori Entity)
        {
            _context.Kategori.Add(Entity);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Kategori Entity)
        {
            _context.Kategori.AddOrUpdate();
        }
    }
}
