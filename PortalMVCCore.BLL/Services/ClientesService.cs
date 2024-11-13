using PortalMVCCore.BLL.Services.Interfaces;
using PortalMVCCore.DAL.Entities.Clientes;
using PortalMVCCore.DAL.Repositories.Interfaces;
using System.Data;
using System.Linq.Expressions;

namespace PortalMVCCore.BLL.Services
{
    public class ClientesService : IClientesService
    {
        IClientesRepository _repository;

        public ClientesService(IClientesRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<CLIENTES_TAB>> GetAll(Expression<Func<CLIENTES_TAB, bool>> filter = null)
        {
            return await _repository.GetAllAsync(filter);
        }

        public async Task<CLIENTES_TAB> FirstOrDefault(Expression<Func<CLIENTES_TAB, bool>> filter = null)
        {
            return await _repository.FirstOrDefaultAsync(filter);
        }

        public async Task<CLIENTES_TAB> Find(params object[] parametros)
        {
            return await _repository.FindAsync(parametros);
        }

        public async Task<CLIENTES_TAB> Add(CLIENTES_TAB entity)
        {
            //var model = await _repository.GetAllAsync();
            //entity.ID_CLIENTE = model.Count() > 0 ? model.Max(p => p.ID_CLIENTE) + 1 : 1;
            return await _repository.AddAsync(entity);
        }

        public async Task Update(CLIENTES_TAB entity)
        {
            await _repository.UpdateAsync(entity);
        }

        public async Task Delete(int Id)
        {
            await _repository.DeleteAsync(Id);
        }


        #region ----- Endereços -----
        public DataTable GetClienteEndereco(int IdCliente, int IdEndereco)
        {
            return _repository.GetClienteEndereco(IdCliente, IdEndereco);
        }

        public async Task AddEditClienteEndereco(int IdCliente, int IdEndereco, string Endereco, string Numero, string Compl, string Bairro, string Cidade, string UF, string Cep)
        {
            CLIENTES_ENDERECOS_TAB? clientes_enderecos_tab = await _repository.FindClienteEndereco(IdCliente, IdEndereco);

            if (clientes_enderecos_tab == null)
            {
                clientes_enderecos_tab = new CLIENTES_ENDERECOS_TAB()
                {
                    ID_CLIENTE = IdCliente,
                    ENDERECO = Endereco,
                    NUMERO = Numero,
                    COMPL = Compl,
                    BAIRRO = Bairro,
                    CIDADE = Cidade,
                    UF = UF,
                    CEP = Cep.Replace("-", "").Replace(".", "")
                };
                await _repository.AddClienteEndereco(clientes_enderecos_tab);
            }
            else
            {
                clientes_enderecos_tab.ENDERECO = Endereco;
                clientes_enderecos_tab.NUMERO = Numero;
                clientes_enderecos_tab.COMPL = Compl;
                clientes_enderecos_tab.BAIRRO = Bairro;
                clientes_enderecos_tab.CIDADE = Cidade;
                clientes_enderecos_tab.UF = UF;
                clientes_enderecos_tab.CEP = Cep.Replace("-", "").Replace(".", "");
                await _repository.UpdateClienteEndereco(clientes_enderecos_tab);
            }
        }

        public async Task DeleteClienteEndereco(int IdCliente, int IdEndereco)
        {
            await _repository.DeleteClienteEndereco(await _repository.FindClienteEndereco(IdCliente, IdEndereco));
        }
        #endregion

        #region ----- Contatos -----
        public DataTable GetClienteContato(int IdCliente, int IdContato)
        {
            return _repository.GetClienteContato(IdCliente, IdContato);
        }

        public async Task AddEditClienteContato(int IdCliente, int IdContato, string Nome, string Fone, string Celular, string Email)
        {
            CLIENTES_CONTATOS_TAB? clientes_contatos_tab = await _repository.FindClienteContato(IdCliente, IdContato);

            if (clientes_contatos_tab == null)
            {
                clientes_contatos_tab = new CLIENTES_CONTATOS_TAB()
                {
                    ID_CLIENTE = IdCliente,
                    NOME = Nome,
                    FONE = Fone,
                    CELULAR = Celular,
                    EMAIL = Email
                };
                await _repository.AddClienteContato(clientes_contatos_tab);
            }
            else
            {
                clientes_contatos_tab.NOME = Nome;
                clientes_contatos_tab.FONE = Fone;
                clientes_contatos_tab.CELULAR = Celular;
                clientes_contatos_tab.EMAIL = Email;
                await _repository.UpdateClienteContato(clientes_contatos_tab);
            }
        }

        public async Task DeleteClienteContato(int IdCliente, int IdContato)
        {
            await _repository.DeleteClienteContato(await _repository.FindClienteContato(IdCliente, IdContato));
        }
        #endregion
    }
}
