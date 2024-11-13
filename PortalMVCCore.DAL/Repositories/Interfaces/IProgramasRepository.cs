using PortalMVCCore.DAL.Entities;
using System.Linq.Expressions;

namespace PortalMVCCore.DAL.Repositories.Interfaces
{
    public interface IProgramasRepository
    {
        Task<PROGRAMAS_TAB> FirstOrDefaultAsync(Expression<Func<PROGRAMAS_TAB, bool>> filter = null);
    }
}
