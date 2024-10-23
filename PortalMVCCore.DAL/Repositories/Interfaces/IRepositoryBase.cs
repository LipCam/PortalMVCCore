using System.Linq.Expressions;

namespace PortalMVCCore.DAL.Repositories.Interfaces
{
    public interface IRepositoryBase<T> where T : class
    {
        List<T> GetAll(Expression<Func<T, bool>> filter = null);
        T FirstOrDefault(Expression<Func<T, bool>> filter = null);
        T Find(params object[] parametros);
        T Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
