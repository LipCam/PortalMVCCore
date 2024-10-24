using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PortalMVCCore.DAL.Entities.Clientes;
using PortalMVCCore.DAL.Repositories.Interfaces;
using PortalMVCCore.Web.Classes.Utilitarios;
using System.Data;

namespace PortalMVCCore.Controllers
{
    [Authorize]
    public class ClientesController : Controller
    {
        private IClientesRepository _repository;

        public ClientesController(IClientesRepository repository)
        {
            _repository = repository;
        }

        [Route("Clientes")]
        public IActionResult Index()
        {
            var IdUsuario = User.FindFirst("IdUsuario")?.Value;
            var Usuario = User.FindFirst("Usuario")?.Value;

            return View();
        }

        public IActionResult grClientesPartial()
        {
            return Ok(_repository.GetAll());
        }

        public void CarregaSelectLists(CLIENTES_TAB clientes_tab)
        {
            ViewBag.TIPO_PESSOA = new SelectList(new[] { new SelectListItem() { Value = "1", Text = "Física" }, new SelectListItem() { Value = "2", Text = "Jurídica" } }, "Value", "Text", clientes_tab != null ? clientes_tab.TIPO_PESSOA : 0);
            ViewBag.UF = new SelectList(new UtlUF().GetUFs(), "UF", "UF");
        }

        [Route("Clientes/Cliente")]
        public IActionResult frmClientes(int Id = 0)
        {
            ViewBag.Id = Id;
            if (Id == 0)
            {
                CarregaSelectLists(null);
                return View();
            }
            else
            {
                CLIENTES_TAB? clientes_tab = _repository.Find(Id);

                if (clientes_tab != null)
                {
                    CarregaSelectLists(clientes_tab);
                    return View(clientes_tab);
                }
                else
                    return RedirectToAction("Index", new { Mensagem = "Falha ao acessar o registro. Registro inexistente." });
            }
        }

        [HttpPost]
        [Route("Clientes/Cliente")]
        [ValidateAntiForgeryToken]
        public IActionResult frmClientes(int Id, CLIENTES_TAB clientes_tab, bool NovoReg = false)
        {
            if (Id == 0)
                _repository.Add(clientes_tab);
            else
                _repository.Update(clientes_tab);
            //ViewBag.id = id;
            //ViewBag.Salvo = true;
            //return View(clientes_tab);

            Id = clientes_tab.ID_CLIENTE;
            if (NovoReg)
                Id = 0;

            return RedirectToAction("frmClientes", new { Id = Id });
        }

        public void Delete(int Id)
        {
            _repository.Delete(Id);
        }

        #region ----- Endereços -----
        public IActionResult grClientesEnderecosPartial(int IdCliente)
        {
            DataTable dt = _repository.GetClienteEndereco(IdCliente, -1);
            return Ok(new ConvertDictionary().GetTableRows(dt));
        }

        public void AddEditClienteEndereco(int IdCliente = 0, int IdEndereco = 0, string Endereco = "", string Numero = "", string Compl = "",
            string Bairro = "", string Cidade = "", string UF = "", string Cep = "")
        {
            CLIENTES_ENDERECOS_TAB? clientes_enderecos_tab = _repository.FindClienteEndereco(IdCliente, IdEndereco);

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
                _repository.AddClienteEndereco(clientes_enderecos_tab);
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
                _repository.UpdateClienteEndereco(clientes_enderecos_tab);
            }
        }

        public void DeleteClienteEndereco(int IdCliente= 0, int IdEndereco = 0)
        {
            _repository.DeleteClienteEndereco(_repository.FindClienteEndereco(IdCliente, IdEndereco));
        }

        public IActionResult GetClienteEndereco(int IdCliente = 0, int IdEndereco = 0)
        {
            DataTable dt = _repository.GetClienteEndereco(IdCliente, IdEndereco);
            return Ok(new ConvertDictionary().GetTableRows(dt));
        }
        #endregion

        #region ----- Contatos -----
        public IActionResult grClientesContatosPartial(int IdCliente)
        {
            DataTable dt = _repository.GetClienteContato(IdCliente, -1);
            return Ok(new ConvertDictionary().GetTableRows(dt));
        }

        public void AddEditClienteContato(int IdCliente = 0, int IdContato = 0, string Nome = "", string Fone = "", string Celular = "", string Email = "")
        {
            CLIENTES_CONTATOS_TAB? clientes_contatos_tab = _repository.FindClienteContato(IdCliente, IdContato);

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
                _repository.AddClienteContato(clientes_contatos_tab);
            }
            else
            {
                clientes_contatos_tab.NOME = Nome;
                clientes_contatos_tab.FONE = Fone;
                clientes_contatos_tab.CELULAR = Celular;
                clientes_contatos_tab.EMAIL = Email;
                _repository.UpdateClienteContato(clientes_contatos_tab);
            }
        }

        public void DeleteClienteContato(int IdCliente = 0, int IdContato = 0)
        {
            _repository.DeleteClienteContato(_repository.FindClienteContato(IdCliente, IdContato));
        }

        public IActionResult GetClienteContato(int IdCliente = 0, int IdContato = 0)
        {
            DataTable dt = _repository.GetClienteContato(IdCliente, IdContato);
            return Ok(new ConvertDictionary().GetTableRows(dt));
        }
        #endregion

        public IActionResult grPesqClientesPartial()
        {
            return Ok(_repository.GetAll());
        }

        public IActionResult GetCliente(int Id = 0, string Descricao = "")
        {
            if (Id == 0 && Descricao == "")
                return Ok();

            return Ok(_repository.GetAll(p => (Id == 0 || p.ID_CLIENTE == Id) && (Descricao == "" || p.NOME.Contains(Descricao))));
        }
    }
}
