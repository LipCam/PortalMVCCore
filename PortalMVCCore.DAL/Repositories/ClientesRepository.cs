using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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

        public async Task DeleteAsync(int IdCliente)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                CLIENTES_TAB? clientes_tab = await _dbContext.CLIENTES_TAB.FindAsync(IdCliente);

                if (clientes_tab != null)
                {
                    IQueryable<CLIENTES_ENDERECOS_TAB> CliEnd = _dbContext.CLIENTES_ENDERECOS_TAB.Where(p => p.ID_CLIENTE == IdCliente).AsNoTracking();
                    _dbContext.CLIENTES_ENDERECOS_TAB.RemoveRange(CliEnd);
                    await _dbContext.SaveChangesAsync();

                    IQueryable<CLIENTES_CONTATOS_TAB> CliContato = _dbContext.CLIENTES_CONTATOS_TAB.Where(p => p.ID_CLIENTE == IdCliente).AsNoTracking();
                    _dbContext.CLIENTES_CONTATOS_TAB.RemoveRange(CliContato);
                    await _dbContext.SaveChangesAsync();

                    _dbContext.CLIENTES_TAB.Remove(clientes_tab);
                    await _dbContext.SaveChangesAsync();

                    await transaction.CommitAsync();
                }
            }
            catch (Exception)
            {
                await transaction.RollbackAsync(); 
                throw;
            }
        }

        #region ----- Endereços -----
        public async Task<CLIENTES_ENDERECOS_TAB?> FindClienteEndereco(int IdCliente, int IdEndereco)
        {
            return await _dbContext.CLIENTES_ENDERECOS_TAB.FindAsync(IdCliente, IdEndereco);
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

        public async Task AddClienteEndereco(CLIENTES_ENDERECOS_TAB clientes_enderecos_tab)
        {
            var Enderecos = await _clienteEnderecosRepositoryBase.GetAllAsync(p => p.ID_CLIENTE == clientes_enderecos_tab.ID_CLIENTE);
            clientes_enderecos_tab.ID_ENDERECO = Enderecos.Count > 0 ? Enderecos.Max(p=> p.ID_ENDERECO) + 1 : 1;
            await _clienteEnderecosRepositoryBase.AddAsync(clientes_enderecos_tab);
        }

        public async Task UpdateClienteEndereco(CLIENTES_ENDERECOS_TAB clientes_enderecos_tab)
        {
            await _clienteEnderecosRepositoryBase.UpdateAsync(clientes_enderecos_tab);
        }

        public async Task DeleteClienteEndereco(CLIENTES_ENDERECOS_TAB clientes_enderecos_tab)
        {
            await _clienteEnderecosRepositoryBase.DeleteAsync(clientes_enderecos_tab);
        }
        #endregion

        #region ----- Contatos -----
        public async Task<CLIENTES_CONTATOS_TAB?> FindClienteContato(int IdCliente, int CodContato)
        {
            return await _dbContext.CLIENTES_CONTATOS_TAB.FindAsync(IdCliente, CodContato);
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

        public async Task AddClienteContato(CLIENTES_CONTATOS_TAB clientes_contatos_tab)
        {
            var Contatos = await _clienteContatosRepositoryBase.GetAllAsync(p => p.ID_CLIENTE == clientes_contatos_tab.ID_CLIENTE);
            clientes_contatos_tab.ID_CONTATO = Contatos.Count > 0 ? Contatos.Max(p => p.ID_CONTATO) + 1 : 1;
            await _clienteContatosRepositoryBase.AddAsync(clientes_contatos_tab);
        }

        public async Task UpdateClienteContato(CLIENTES_CONTATOS_TAB clientes_contatos_tab)
        {
            await _clienteContatosRepositoryBase.UpdateAsync(clientes_contatos_tab);
        }

        public async Task DeleteClienteContato(CLIENTES_CONTATOS_TAB clientes_contatos_tab)
        {
            await _clienteContatosRepositoryBase.DeleteAsync(clientes_contatos_tab);
        }
        #endregion
    }
}
