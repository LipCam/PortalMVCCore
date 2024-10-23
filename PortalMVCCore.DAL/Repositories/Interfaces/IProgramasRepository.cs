using PortalMVCCore.DAL.Entities;
using System.Linq.Expressions;

namespace PortalMVCCore.DAL.Repositories.Interfaces
{
    public interface IProgramasRepository
    {
        List<PROGRAMAS_TAB> GetAll(Expression<Func<PROGRAMAS_TAB, bool>> filter = null);
    }
}
