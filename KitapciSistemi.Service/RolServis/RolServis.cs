using KitapciSistemi.Core.DataBase.Entities;
using KitapciSistemi.Data.DataContext;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KitapciSistemi.Service.RolServis
{
    public class RolServis:IRolServis
    {
        private readonly KitapciContext _context = new KitapciContext();
        public int Count()
        {
            return _context.Rol.Count();
        }

        public void Delete(int id)
        {
            var Rol = GetById(id);
            if (Rol != null)
            {
                _context.Rol.Remove(Rol);
            }
        }

        public Rol Get(Expression<Func<Rol, bool>> expression)
        {
            return _context.Rol.FirstOrDefault(expression);
        }

        public IEnumerable<Rol> GetAll()
        {
            return _context.Rol.Select(x => x);
        }

        public Rol GetById(int id)
        {
            return _context.Rol.FirstOrDefault(x => x.ID == id);
        }

        public IQueryable<Rol> GetMany(Expression<Func<Rol, bool>> expression)
        {
            return _context.Rol.Where(expression);
        }

        public void Insert(Rol Entity)
        {
            _context.Rol.Add(Entity);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Rol Entity)
        {
            _context.Rol.AddOrUpdate();
        }
    }
}
