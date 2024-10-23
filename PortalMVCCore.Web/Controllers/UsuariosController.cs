using PortalMVCCore.DAL.Entities;
using PortalMVCCore.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PortalMVCCore.Web.Controllers
{
    [Authorize]
    public class UsuariosController : Controller
    {
        private IUsuariosRepository _repository;

        public UsuariosController(IUsuariosRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index(string Mensagem = "")
        {
            int.TryParse(User.FindFirst("CodEmpresa")?.Value, out int CodEmpresa);

            if (CodEmpresa != 0)
                return RedirectToAction("AcessoNegado", "Home");

            ViewBag.Mensagem = Mensagem;

            return View();
        }

        public IActionResult grUsuariosPartial()
        {
            return Ok(_repository.GetAll());
        }

        [Route("Usuarios/Usuario")]
        public IActionResult frmUsuarios(int Id = 0)
        {
            ViewBag.id = Id;

            if (Id == 0)
                return View();
            else
            {
                USUARIOS_TAB usuarios_tab = _repository.Find(Id);

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
        public IActionResult frmUsuarios(int Id, USUARIOS_TAB usuarios_tab, bool NovoReg = false)
        {
            if (Id == 0)
            {
                usuarios_tab.COD_USUARIO = _repository.GetAll().Count() > 0 ? _repository.GetAll().Max(p => p.COD_USUARIO) + 1 : 1;
                _repository.Add(usuarios_tab);
            }
            else
                _repository.Update(usuarios_tab);

            Id = usuarios_tab.COD_USUARIO;
            if (NovoReg)
                Id = 0;

            return RedirectToAction("frmUsuarios", new { Id = Id });
        }

        public void Delete(int Id)
        {
            USUARIOS_TAB usuarios_tab = _repository.Find(Id);
            _repository.Delete(usuarios_tab);
        }
    }
}
