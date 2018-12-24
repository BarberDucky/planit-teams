using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace planit_data.Repository
{
    public interface IRepository<T> where T:class
    {
        List<T> GetAll();
        T GetById(int id);
        bool Insert(T obj);
        void Update(T obj);
        bool Delete(int id);
        IEnumerable<T> Get(Expression<Func<T, bool>> filter = null,
         Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
         string includeProperties = "");
    }
}
