using PortalMVCCore.BLL.Services.Interfaces;
using PortalMVCCore.DAL.Entities;
using PortalMVCCore.DAL.Repositories.Interfaces;
using System.Linq.Expressions;

namespace PortalMVCCore.BLL.Services
{
    public class UsuariosService : IUsuariosService
    {
        IUsuariosRepository _repository;

        public UsuariosService(IUsuariosRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<USUARIOS_TAB>> GetAll(Expression<Func<USUARIOS_TAB, bool>> filter = null)
        {
            return await _repository.GetAllAsync(filter);
        }

        public async Task<USUARIOS_TAB> FirstOrDefault(Expression<Func<USUARIOS_TAB, bool>> filter = null)
        {
            return await _repository.FirstOrDefaultAsync(filter);
        }

        public async Task<USUARIOS_TAB> Find(params object[] parametros)
        {
            return await _repository.FindAsync(parametros);
        }

        public async Task<USUARIOS_TAB> Add(USUARIOS_TAB entity)
        {
            var model = await _repository.GetAllAsync();
            entity.ID_USUARIO = model.Count() > 0 ? model.Max(p => p.ID_USUARIO) + 1 : 1;
            return await _repository.AddAsync(entity);
        }

        public async Task Update(USUARIOS_TAB entity)
        {
            await _repository.UpdateAsync(entity);
        }

        public async Task Delete(int Id)
        {
            USUARIOS_TAB entity = await _repository.FindAsync(Id);
            await _repository.DeleteAsync(entity);
        }
    }
}
