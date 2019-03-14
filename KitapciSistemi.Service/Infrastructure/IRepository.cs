using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KitapciSistemi.Service.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        void Insert(T Entity);
        void Update(T Entity);
        void Delete(int id);
        int Count();
        void Save();
        IEnumerable<T> GetAll();
        T GetById(int id);
        T Get(Expression<Func<T, bool>> expression);
        IQueryable<T> GetMany(Expression<Func<T, bool>> expression);
    }
}
