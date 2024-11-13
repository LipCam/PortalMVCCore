using PortalMVCCore.DAL.Entities.Clientes;
using System.Data;
using System.Linq.Expressions;

namespace PortalMVCCore.BLL.Services.Interfaces
{
    public interface IClientesService
    {
        Task<List<CLIENTES_TAB>> GetAll(Expression<Func<CLIENTES_TAB, bool>> filter = null);
        Task<CLIENTES_TAB> FirstOrDefault(Expression<Func<CLIENTES_TAB, bool>> filter = null);
        Task<CLIENTES_TAB> Find(params object[] parametros);
        Task<CLIENTES_TAB> Add(CLIENTES_TAB entity);
        Task Update(CLIENTES_TAB entity);
        Task Delete(int Id);

        #region ----- Endereços -----
        DataTable GetClienteEndereco(int IdCliente, int IdEndereco);
        Task AddEditClienteEndereco(int IdCliente, int IdEndereco, string Endereco, string Numero, string Compl, string Bairro, string Cidade, string UF, string Cep);
        Task DeleteClienteEndereco(int IdCliente, int IdEndereco);
        #endregion

        #region ----- Contatos -----
        DataTable GetClienteContato(int IdCliente, int IdContato);
        Task AddEditClienteContato(int IdCliente, int IdContato, string Nome, string Fone, string Celular, string Email);
        Task DeleteClienteContato(int IdCliente, int IdContato);
        #endregion
    }
}
