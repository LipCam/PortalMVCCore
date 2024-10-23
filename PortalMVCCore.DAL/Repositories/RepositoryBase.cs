using Microsoft.EntityFrameworkCore;
using PortalMVCCore.DAL.DB;
using PortalMVCCore.DAL.Repositories.Interfaces;
using System.Linq.Expressions;

namespace PortalMVCCore.DAL.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected AppDbContext _dbContext;

        public RepositoryBase(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<T> GetAll(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.ToList();
        }

        public T FirstOrDefault(Expression<Func<T, bool>> filter = null)
        {
            return _dbContext.Set<T>().FirstOrDefault(filter);
        }

        public T Find(params object[] keyValues)
        {
            return _dbContext.Set<T>().Find(keyValues);
        }

        public T Add(T entity)
        {
            if (entity != null)
            {
                _dbContext.Set<T>().Add(entity);
                _dbContext.SaveChanges();
            }

            return entity;
        }

        public void Update(T entity)
        {
            if (entity != null)
            {
                _dbContext.Entry(entity).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
        }

        public void Delete(T entity)
        {
            if (entity != null)
            {
                _dbContext.Set<T>().Remove(entity);
                _dbContext.SaveChanges();
            }
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
