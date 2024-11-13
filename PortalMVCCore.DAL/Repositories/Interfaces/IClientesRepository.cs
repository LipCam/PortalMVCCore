using PortalMVCCore.DAL.Entities.Clientes;
using System.Data;

namespace PortalMVCCore.DAL.Repositories.Interfaces
{
    public interface IClientesRepository : IRepositoryBase<CLIENTES_TAB>
    {
        Task DeleteAsync(int IdCliente);

        #region ----- Endereços -----
        Task<CLIENTES_ENDERECOS_TAB?> FindClienteEndereco(int IdCliente, int IdEndereco);
        DataTable GetClienteEndereco(int IdCliente, int IdEndereco);
        Task AddClienteEndereco(CLIENTES_ENDERECOS_TAB clientes_enderecos_tab);
        Task UpdateClienteEndereco(CLIENTES_ENDERECOS_TAB clientes_enderecos_tab);
        Task DeleteClienteEndereco(CLIENTES_ENDERECOS_TAB? clientes_enderecos_tab);
        #endregion

        #region ----- Contatos -----
        Task<CLIENTES_CONTATOS_TAB?> FindClienteContato(int IdCliente, int IdContato);
        DataTable GetClienteContato(int IdCliente, int IdContato);
        Task AddClienteContato(CLIENTES_CONTATOS_TAB clientes_contatos_tab);
        Task UpdateClienteContato(CLIENTES_CONTATOS_TAB clientes_contatos_tab);
        Task DeleteClienteContato(CLIENTES_CONTATOS_TAB? clientes_contatos_tab);
        #endregion
    }
}
