using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PortalMVCCore.BLL.Services.Interfaces;
using PortalMVCCore.DAL.Entities.Clientes;
using PortalMVCCore.Web.Classes.Utilitarios;
using System.Data;

namespace PortalMVCCore.Controllers
{
    [Authorize]
    public class ClientesController : Controller
    {
        private IClientesService _service;

        public ClientesController(IClientesService service)
        {
            _service = service;
        }

        [Route("Clientes")]
        public IActionResult Index()
        {
            var IdUsuario = User.FindFirst("IdUsuario")?.Value;
            var Usuario = User.FindFirst("Usuario")?.Value;

            return View();
        }

        public async Task<IActionResult> grClientesPartial()
        {
            return Ok(await _service.GetAll());
        }

        public void CarregaSelectLists(CLIENTES_TAB clientes_tab)
        {
            ViewBag.TIPO_PESSOA = new SelectList(new[] { new SelectListItem() { Value = "1", Text = "Física" }, new SelectListItem() { Value = "2", Text = "Jurídica" } }, "Value", "Text", clientes_tab != null ? clientes_tab.TIPO_PESSOA : 0);
            ViewBag.UF = new SelectList(new UtlUF().GetUFs(), "UF", "UF");
        }

        [Route("Clientes/Cliente")]
        public async Task<IActionResult> frmClientes(int Id = 0)
        {
            ViewBag.Id = Id;
            if (Id == 0)
            {
                CarregaSelectLists(null);
                return View();
            }
            else
            {
                CLIENTES_TAB? clientes_tab = await _service.Find(Id);

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
        public async Task<IActionResult> frmClientes(int Id, CLIENTES_TAB clientes_tab, bool NovoReg = false)
        {
            if (Id == 0)
                await _service.Add(clientes_tab);
            else
                await _service.Update(clientes_tab);
            //ViewBag.id = id;
            //ViewBag.Salvo = true;
            //return View(clientes_tab);

            Id = clientes_tab.ID_CLIENTE;
            if (NovoReg)
                Id = 0;

            return RedirectToAction("frmClientes", new { Id = Id });
        }

        public async Task Delete(int Id)
        {
            await _service.Delete(Id);
        }

        #region ----- Endereços -----
        public IActionResult grClientesEnderecosPartial(int IdCliente)
        {
            DataTable dt = _service.GetClienteEndereco(IdCliente, -1);
            return Ok(new ConvertDictionary().GetTableRows(dt));
        }

        public async Task AddEditClienteEndereco(int IdCliente = 0, int IdEndereco = 0, string Endereco = "", string Numero = "", string Compl = "",
            string Bairro = "", string Cidade = "", string UF = "", string Cep = "")
        {
            await _service.AddEditClienteEndereco(IdCliente, IdEndereco, Endereco, Numero, Compl, Bairro, Cidade, UF, Cep);
        }

        public async Task DeleteClienteEndereco(int IdCliente= 0, int IdEndereco = 0)
        {
            await _service.DeleteClienteEndereco(IdCliente, IdEndereco);
        }

        public IActionResult GetClienteEndereco(int IdCliente = 0, int IdEndereco = 0)
        {
            DataTable dt = _service.GetClienteEndereco(IdCliente, IdEndereco);
            return Ok(new ConvertDictionary().GetTableRows(dt));
        }
        #endregion

        #region ----- Contatos -----
        public IActionResult grClientesContatosPartial(int IdCliente)
        {
            DataTable dt = _service.GetClienteContato(IdCliente, -1);
            return Ok(new ConvertDictionary().GetTableRows(dt));
        }

        public async Task AddEditClienteContato(int IdCliente = 0, int IdContato = 0, string Nome = "", string Fone = "", string Celular = "", string Email = "")
        {
            await _service.AddEditClienteContato(IdCliente, IdContato, Nome, Fone, Celular, Email);
        }

        public async Task DeleteClienteContato(int IdCliente = 0, int IdContato = 0)
        {
            await _service.DeleteClienteContato(IdCliente, IdContato);
        }

        public IActionResult GetClienteContato(int IdCliente = 0, int IdContato = 0)
        {
            DataTable dt = _service.GetClienteContato(IdCliente, IdContato);
            return Ok(new ConvertDictionary().GetTableRows(dt));
        }
        #endregion

        public async Task<IActionResult> grPesqClientesPartial()
        {
            return Ok(await _service.GetAll());
        }

        public async Task<IActionResult> GetCliente(int Id = 0, string Descricao = "")
        {
            if (Id == 0 && Descricao == "")
                return Ok();

            return Ok(await _service.GetAll(p => (Id == 0 || p.ID_CLIENTE == Id) && (Descricao == "" || p.NOME.Contains(Descricao))));
        }
    }
}
