using PortalMVCCore.DAL.Entities;
using System.Linq.Expressions;

namespace PortalMVCCore.BLL.Services.Interfaces
{
    public interface IUsuariosService
    {
        Task<List<USUARIOS_TAB>> GetAll(Expression<Func<USUARIOS_TAB, bool>> filter = null);
        Task<USUARIOS_TAB> FirstOrDefault(Expression<Func<USUARIOS_TAB, bool>> filter = null);
        Task<USUARIOS_TAB> Find(params object[] parametros);
        Task<USUARIOS_TAB> Add(USUARIOS_TAB entity);
        Task Update(USUARIOS_TAB entity);
        Task Delete(int Id);
    }
}
