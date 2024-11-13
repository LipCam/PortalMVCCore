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

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.ToList();
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> filter = null)
        {
            return await _dbContext.Set<T>().FirstOrDefaultAsync(filter);
        }

        public async Task<T> FindAsync(params object[] keyValues)
        {
            return await _dbContext.Set<T>().FindAsync(keyValues);
        }

        public async Task<T> AddAsync(T entity)
        {
            if (entity != null)
            {
                await _dbContext.Set<T>().AddAsync(entity);
                await SaveChanges();
            }

            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            if (entity != null)
            {
                _dbContext.Entry(entity).State = EntityState.Modified;
                await SaveChanges();
            }
        }

        public async Task DeleteAsync(T entity)
        {
            if (entity != null)
            {
                _dbContext.Set<T>().Remove(entity);
                await SaveChanges();
            }
        }

        public async Task SaveChanges()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
