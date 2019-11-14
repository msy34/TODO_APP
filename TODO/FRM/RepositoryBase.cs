using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TODO.FRM
{
    public class RepositoryBase<T> : IQueryable<T>
        where T : class
    {
        private readonly ToDoContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public Expression Expression
        {
            get { return RepositoryBase<T>.u200f⁯⁫‍​‏​‍‌​‮‌‭⁬⁫‭⁭‬‫‪‬‮‏⁯⁫⁮‬‫‫⁮‏⁮⁯⁪⁯‌‭‬‍‎‮(this.Queryable()); }
        }

        public IQueryable<T> Queryable()
        {
            return _dbSet.AsQueryable();
        }

        public IQueryProvider Provider
        {
            get { return RepositoryBase<T>.u200d‫‭⁯‏⁮‪⁫‭⁯‬‭‭‭‎‫‭‮⁭‬⁬‏‭‏‭‬‌⁯⁫‬⁮‎⁪⁬‪‎⁬‌‪⁭‮(this.Queryable()); }
        }

        public Type ElementType
        {
            get { return RepositoryBase<T>.u202a‫⁪‬‭⁯‫⁮⁭⁫‎‭‫⁭‫‬⁫⁫‮‍‌‎⁮‎​⁫‍⁬‍⁭‪‫⁮‮‪⁭⁫‍‍⁬‮(this.Queryable()); }
        }

        // privatescope
        internal static IQueryProvider u200d‫‭⁯‏⁮‪⁫‭⁯‬‭‭‭‎‫‭‮⁭‬⁬‏‭‏‭‬‌⁯⁫‬⁮‎⁪⁬‪‎⁬‌‪⁭‮(IQueryable queryables)
        {
            return queryables.Provider;
        }

        // ‪‫⁪‬‭⁯‫⁮⁭⁫‎‭‫⁭‫‬⁫⁫‮‍‌‎⁮‎​⁫‍⁬‍⁭‪‫⁮‮‪⁭⁫‍‍⁬‮
        // privatescope
        internal static Type u202a‫⁪‬‭⁯‫⁮⁭⁫‎‭‫⁭‫‬⁫⁫‮‍‌‎⁮‎​⁫‍⁬‍⁭‪‫⁮‮‪⁭⁫‍‍⁬‮(IQueryable queryables)
        {
            return queryables.ElementType;
        }

        // ‏⁯⁫‍​‏​‍‌​‮‌‭⁬⁫‭⁭‬‫‪‬‮‏⁯⁫⁮‬‫‫⁮‏⁮⁯⁪⁯‌‭‬‍‎‮
        // privatescope
        internal static Expression u200f⁯⁫‍​‏​‍‌​‮‌‭⁬⁫‭⁭‬‫‪‬‮‏⁯⁫⁮‬‫‫⁮‏⁮⁯⁪⁯‌‭‬‍‎‮(IQueryable queryables)
        {
            return queryables.Expression;
        }

        public RepositoryBase(ToDoContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException("dbContext can not be null.");

            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public RepositoryBase()
        {
        }

        #region IRepository Members

        public IQueryable<T> GetAll()
        {
            return _dbSet;
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public T GetByKey(int id)
        {
            return _dbSet.Find(id);
        }

        public T Get(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).SingleOrDefault();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            // Eğer sizlerde genelde bir kayıtı silmek yerine IsDelete şeklinde bool bir flag alanı tutuyorsanız,
            // Küçük bir refleciton kodu yardımı ile bunuda otomatikleştirebiliriz.
            if (entity.GetType().GetProperty("IsDelete") != null)
            {
                T _entity = entity;

                _entity.GetType().GetProperty("IsDelete").SetValue(_entity, true);

                this.Update(_entity);
            }
            else
            {
                // Önce entity'nin state'ini kontrol etmeliyiz.
                DbEntityEntry dbEntityEntry = _dbContext.Entry(entity);

                if (dbEntityEntry.State != EntityState.Deleted)
                {
                    dbEntityEntry.State = EntityState.Deleted;
                }
                else
                {
                    _dbSet.Attach(entity);
                    _dbSet.Remove(entity);
                }
            }
        }

        public void Delete(int id)
        {
            var entity = GetByKey(id);
            if (entity == null) return;
            else
            {
                if (entity.GetType().GetProperty("IsDelete") != null)
                {
                    T _entity = entity;
                    _entity.GetType().GetProperty("IsDelete").SetValue(_entity, true);

                    this.Update(_entity);
                }
                else
                {
                    Delete(entity);
                }
            }
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.Queryable().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator) this.Queryable().GetEnumerator();
        }


        public IQueryable<T> Search(Expression<Func<T, bool>> predicate)
        {
            return this.Queryable().Where<T>(predicate);
        }



        #endregion


    }
}
