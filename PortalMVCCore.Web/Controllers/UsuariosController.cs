using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalMVCCore.BLL.Services.Interfaces;
using PortalMVCCore.DAL.Entities;

namespace PortalMVCCore.Web.Controllers
{
    [Authorize]
    public class UsuariosController : Controller
    {
        private IUsuariosService _service;

        public UsuariosController(IUsuariosService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index(string Mensagem = "")
        {
            int.TryParse(User.FindFirst("CodEmpresa")?.Value, out int CodEmpresa);

            if (CodEmpresa != 0)
                return RedirectToAction("AcessoNegado", "Home");

            ViewBag.Mensagem = Mensagem;

            return View();
        }

        public async Task<IActionResult> grUsuariosPartial()
        {
            return Ok(await _service.GetAll());
        }

        [Route("Usuarios/Usuario")]
        public async Task<IActionResult> frmUsuarios(int Id = 0)
        {
            ViewBag.id = Id;

            if (Id == 0)
                return View();
            else
            {
                USUARIOS_TAB usuarios_tab = await _service.Find(Id);

                if (usuarios_tab != null)
                {
                    return View(usuarios_tab);
                }
                else
                    return RedirectToAction("Index", new { Mensagem = "Falha ao acessar o registro. Registro inexistente." });
            }
        }

        [HttpPost]
        [Route("Usuarios/Usuario")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> frmUsuarios(int Id, USUARIOS_TAB usuarios_tab, bool NovoReg = false)
        {
            if (Id == 0)
                await _service.Add(usuarios_tab);
            else
                await _service.Update(usuarios_tab);

            Id = usuarios_tab.ID_USUARIO;
            if (NovoReg)
                Id = 0;

            return RedirectToAction("frmUsuarios", new { Id = Id });
        }

        public async Task Delete(int Id)
        {
            await _service.Delete(Id);
        }
    }
}
