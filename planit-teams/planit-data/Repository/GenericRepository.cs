using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace planit_data.Repository
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        protected ApplicationContext context;
        protected DbSet<T> set;

        public GenericRepository(ApplicationContext context)
        {
            this.context = context;
            this.set = context.Set<T>();
        }

        public List<T> GetAll()
        {
            return set.ToList();
        }

        public virtual IEnumerable<T> Get(
          Expression<Func<T, bool>> filter = null,
          Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
          string includeProperties = "")
        {
            IQueryable<T> query = set;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public T GetById(int id)
        {
            return set.Find(id);
        }

        public bool Insert(T obj)
        {
            T retObj = set.Add(obj);
            set.Attach(obj);
            context.Entry(obj).State = EntityState.Added;
            if (retObj != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Update(T obj)
        {
            set.Attach(obj);
            context.Entry(obj).State = EntityState.Modified;
        }

        public bool Delete(int id)
        {
            T obj = set.Find(id);
            if (obj != null)
            {
                set.Remove(obj);
                return true;
            }
            else
            {
                return false;
            }

        }

    }
}
