using PortalMVCCore.DAL.Entities;
using PortalMVCCore.DAL.Repositories.Interfaces;
using System.Linq.Expressions;

namespace PortalMVCCore.DAL.Repositories
{
    public class ProgramasRepository : IProgramasRepository
    {
        private IRepositoryBase<PROGRAMAS_TAB> _repositoryBase;

        public ProgramasRepository(IRepositoryBase<PROGRAMAS_TAB> repositoryBase)
        {
            _repositoryBase = repositoryBase;
        }

        public async Task<PROGRAMAS_TAB> FirstOrDefaultAsync(Expression<Func<PROGRAMAS_TAB, bool>> filter)
        {
            return await _repositoryBase.FirstOrDefaultAsync(filter);
        }
    }
}
