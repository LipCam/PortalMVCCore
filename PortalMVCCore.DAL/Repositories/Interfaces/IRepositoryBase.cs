using System.Linq.Expressions;

namespace PortalMVCCore.DAL.Repositories.Interfaces
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> filter = null);
        Task<T> FindAsync(params object[] parametros);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
