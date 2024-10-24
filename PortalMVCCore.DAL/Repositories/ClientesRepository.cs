using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PortalMVCCore.DAL.DB;
using PortalMVCCore.DAL.Entities.Clientes;
using PortalMVCCore.DAL.Repositories.Interfaces;
using System.Data;

namespace PortalMVCCore.DAL.Repositories
{
    public class ClientesRepository : RepositoryBase<CLIENTES_TAB>, IClientesRepository
    {
        private IConfiguration _configuration;
        private IRepositoryBase<CLIENTES_ENDERECOS_TAB> _clienteEnderecosRepositoryBase;
        private IRepositoryBase<CLIENTES_CONTATOS_TAB> _clienteContatosRepositoryBase;

        public ClientesRepository(AppDbContext dbContext, IConfiguration configuration, 
            IRepositoryBase<CLIENTES_ENDERECOS_TAB> clienteEnderecosRepositoryBase,
            IRepositoryBase<CLIENTES_CONTATOS_TAB> clienteContatossRepositoryBase) : base(dbContext)
        {
            _configuration = configuration;
            _clienteEnderecosRepositoryBase = clienteEnderecosRepositoryBase;
            _clienteContatosRepositoryBase = clienteContatossRepositoryBase;
        }

        public void Delete(int IdCliente)
        {
            CLIENTES_TAB? clientes_tab = _dbContext.CLIENTES_TAB.Find(IdCliente);

            if(clientes_tab != null)
            {
                IQueryable<CLIENTES_ENDERECOS_TAB> CliEnd = _dbContext.CLIENTES_ENDERECOS_TAB.Where(p => p.ID_CLIENTE == IdCliente);
                _dbContext.CLIENTES_ENDERECOS_TAB.RemoveRange(CliEnd);
                _dbContext.SaveChanges();

                IQueryable<CLIENTES_CONTATOS_TAB> CliContato = _dbContext.CLIENTES_CONTATOS_TAB.Where(p => p.ID_CLIENTE == IdCliente);
                _dbContext.CLIENTES_CONTATOS_TAB.RemoveRange(CliContato);
                _dbContext.SaveChanges();

                _dbContext.CLIENTES_TAB.Remove(clientes_tab);
                _dbContext.SaveChanges();
            }
        }

        #region ----- Endereços -----
        public CLIENTES_ENDERECOS_TAB? FindClienteEndereco(int IdCliente, int IdEndereco)
        {
            return _dbContext.CLIENTES_ENDERECOS_TAB.Find(IdCliente, IdEndereco);
        }

        public DataTable GetClienteEndereco(int IdCliente, int IdEndereco)
        {
            string Erro = "";
            string SQL = @"SELECT * 
                            FROM CLIENTES_ENDERECOS_TAB
                            WHERE ID_CLIENTE = @ID_CLIENTE AND (@ID_ENDERECO = -1 OR ID_ENDERECO = @ID_ENDERECO)";

            List<SqlParameter> lst = new List<SqlParameter>()
            {
                new SqlParameter { ParameterName = "@ID_CLIENTE", SqlDbType = SqlDbType.Int, Value = IdCliente },
                new SqlParameter { ParameterName = "@ID_ENDERECO", SqlDbType = SqlDbType.Int, Value = IdEndereco },
            };

            DataTable dt = new ExecuteQuery(_configuration).ExecQueryDT(SQL, lst, out Erro);
            return dt;
        }

        public void AddClienteEndereco(CLIENTES_ENDERECOS_TAB clientes_enderecos_tab)
        {
            var Enderecos = _clienteEnderecosRepositoryBase.GetAll(p => p.ID_CLIENTE == clientes_enderecos_tab.ID_CLIENTE);
            clientes_enderecos_tab.ID_ENDERECO = Enderecos.Count > 0 ? Enderecos.Max(p=> p.ID_ENDERECO) + 1 : 1;
            _clienteEnderecosRepositoryBase.Add(clientes_enderecos_tab);
        }

        public void UpdateClienteEndereco(CLIENTES_ENDERECOS_TAB clientes_enderecos_tab)
        {
            _clienteEnderecosRepositoryBase.Update(clientes_enderecos_tab);
        }

        public void DeleteClienteEndereco(CLIENTES_ENDERECOS_TAB clientes_enderecos_tab)
        {
            _clienteEnderecosRepositoryBase.Delete(clientes_enderecos_tab);
        }
        #endregion

        #region ----- Contatos -----
        public CLIENTES_CONTATOS_TAB? FindClienteContato(int IdCliente, int CodContato)
        {
            return _dbContext.CLIENTES_CONTATOS_TAB.Find(IdCliente, CodContato);
        }

        public DataTable GetClienteContato(int IdCliente, int IdContato)
        {
            string Erro = "";
            string SQL = @"SELECT * 
                            FROM CLIENTES_CONTATOS_TAB
                            WHERE ID_CLIENTE = @ID_CLIENTE AND (@ID_CONTATO = -1 OR ID_CONTATO = @ID_CONTATO)";

            List<SqlParameter> lst = new List<SqlParameter>()
            {
                new SqlParameter { ParameterName = "@ID_CLIENTE", SqlDbType = SqlDbType.Int, Value = IdCliente },
                new SqlParameter { ParameterName = "@ID_CONTATO", SqlDbType = SqlDbType.Int, Value = IdContato },
            };

            DataTable dt = new ExecuteQuery(_configuration).ExecQueryDT(SQL, lst, out Erro);
            return dt;
        }

        public void AddClienteContato(CLIENTES_CONTATOS_TAB clientes_contatos_tab)
        {
            var Contatos = _clienteContatosRepositoryBase.GetAll(p => p.ID_CLIENTE == clientes_contatos_tab.ID_CLIENTE);
            clientes_contatos_tab.ID_CONTATO = Contatos.Count > 0 ? Contatos.Max(p => p.ID_CONTATO) + 1 : 1;
            _clienteContatosRepositoryBase.Add(clientes_contatos_tab);
        }

        public void UpdateClienteContato(CLIENTES_CONTATOS_TAB clientes_contatos_tab)
        {
            _clienteContatosRepositoryBase.Update(clientes_contatos_tab);
        }

        public void DeleteClienteContato(CLIENTES_CONTATOS_TAB clientes_contatos_tab)
        {
            _clienteContatosRepositoryBase.Delete(clientes_contatos_tab);
        }
        #endregion
    }
}
