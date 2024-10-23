using PortalMVCCore.DAL.DB;
using PortalMVCCore.DAL.Entities;
using PortalMVCCore.DAL.Repositories.Interfaces;

namespace PortalMVCCore.DAL.Repositories
{
    public class UsuariosRepository : RepositoryBase<USUARIOS_TAB>, IUsuariosRepository
    {
        public UsuariosRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
