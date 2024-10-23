using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalMVCCore.DAL.Entities;
using PortalMVCCore.DAL.Repositories.Interfaces;

namespace PortalMVCCore.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private IHomeRepository _homeRepository;
        private IUsuariosRepository _usuariosRepository;
        private IProgramasRepository _programasRepository;

        public HomeController(IHomeRepository homeRepository, IUsuariosRepository usuariosRepository, IProgramasRepository programasRepository)
        {
            _homeRepository = homeRepository;
            _usuariosRepository = usuariosRepository;
            _programasRepository = programasRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public class DadosHeader
        {
            public string? IdUsuario { get; set; }
            public string? Usuario { get; set; }
            public string? Empresa { get; set; }
            public string? NomeController { get; set; }
            public string? DescricaoController { get; set; }
            public int IdTipoLogin { get; set; }
            public int IntervNotific { get; set; }
        }

        public IActionResult GetDadosHeader(string NomeController = "")
        {
            DadosHeader Dados = new DadosHeader();
            int.TryParse(User.FindFirst("IdUsuario")?.Value, out int IdUsuario);
            int.TryParse(User.FindFirst("IdTipoLogin")?.Value, out int IdTipoLogin);

            Dados.IdUsuario = IdUsuario.ToString();
            Dados.Usuario = "";

            USUARIOS_TAB usuarios_tab = _usuariosRepository.Find(IdUsuario);

            if (usuarios_tab != null)
                Dados.Usuario = usuarios_tab.NOME.Count() <= 26 ? usuarios_tab.NOME : usuarios_tab.NOME.Substring(0, 26);

            PROGRAMAS_TAB programas_tab = _programasRepository.GetAll(p => p.NOME_CONTROLLER == NomeController).FirstOrDefault();
            Dados.DescricaoController = programas_tab != null ? programas_tab.DESCRICAO : "";

            Dados.IdTipoLogin = IdTipoLogin;

            return Json(Dados);
        }

        public IActionResult GetDashboard()
        {
            int.TryParse(User.FindFirst("IdUsuario")?.Value, out int IdUsuario);
            int.TryParse(User.FindFirst("CodEmpresa")?.Value, out int CodEmpresa);
            int.TryParse(User.FindFirst("IdTipoLogin")?.Value, out int IdTipoLogin);

            return Json(_homeRepository.GetDashBoard(IdUsuario, CodEmpresa, IdTipoLogin));
        }
    }
}
