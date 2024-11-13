using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalMVCCore.BLL.Services.Interfaces;
using PortalMVCCore.DAL.DTOs;

namespace PortalMVCCore.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private IHomeService _homeService;

        public HomeController(IHomeService homeService)
        {
            _homeService = homeService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetDadosHeader(string NomeController = "")
        {
            int.TryParse(User.FindFirst("IdUsuario")?.Value, out int IdUsuario);
            int.TryParse(User.FindFirst("IdTipoLogin")?.Value, out int IdTipoLogin);

            DadosHeaderDTO Dados = await _homeService.GetDadosHeader(NomeController, IdUsuario, IdTipoLogin);

            return Json(Dados);
        }

        public IActionResult GetDashboard()
        {
            return Json(_homeService.GetDashBoard());
        }
    }
}
