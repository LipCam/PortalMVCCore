using PortalMVCCore.DAL.Entities.Clientes;
using System.Data;

namespace PortalMVCCore.DAL.Repositories.Interfaces
{
    public interface IClientesRepository : IRepositoryBase<CLIENTES_TAB>
    {
        void Delete(int IdCliente);

        #region ----- Endereços -----
        CLIENTES_ENDERECOS_TAB? FindClienteEndereco(int IdCliente, int IdEndereco);
        DataTable GetClienteEndereco(int IdCliente, int IdEndereco);
        void AddClienteEndereco(CLIENTES_ENDERECOS_TAB clientes_enderecos_tab);
        void UpdateClienteEndereco(CLIENTES_ENDERECOS_TAB clientes_enderecos_tab);
        void DeleteClienteEndereco(CLIENTES_ENDERECOS_TAB? clientes_enderecos_tab);
        #endregion

        #region ----- Contatos -----
        CLIENTES_CONTATOS_TAB? FindClienteContato(int IdCliente, int IdContato);
        DataTable GetClienteContato(int IdCliente, int IdContato);
        void AddClienteContato(CLIENTES_CONTATOS_TAB clientes_contatos_tab);
        void UpdateClienteContato(CLIENTES_CONTATOS_TAB clientes_contatos_tab);
        void DeleteClienteContato(CLIENTES_CONTATOS_TAB? clientes_contatos_tab);
        #endregion
    }
}
