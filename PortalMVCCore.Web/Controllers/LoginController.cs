using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PortalMVCCore.BLL.Services.Interfaces;
using PortalMVCCore.DAL.Entities;
using System.Security.Claims;

namespace PortalMVCCore.Controllers
{
    public class LoginController : Controller
    {
        private IUsuariosService _usuariosService;

        public LoginController(IUsuariosService usuariosService)
        {
            _usuariosService = usuariosService;
        }

        public class TipoLogin
        {
            public int IdTipoLogin { get; set; }
            public string Descricao { get; set; }
        }

        public void CarregaSelectList()
        {
            List<TipoLogin> lst = new List<TipoLogin>(){new TipoLogin { IdTipoLogin = 0, Descricao = "Administrador" },
                                                    new TipoLogin { IdTipoLogin = 1, Descricao = "Cliente" },
                                                    new TipoLogin { IdTipoLogin = 2, Descricao = "Consumidor" }};

            ViewBag.IdTipoLogin = new SelectList(lst, "IdTipoLogin", "Descricao");
        }

        public IActionResult Index()
        {
            ClaimsPrincipal claims = HttpContext.User;
            if(claims.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            CarregaSelectList();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(USUARIOS_TAB usuarios_tab, int IdTipoLogin = 0)
        {
            string Erro = "";

            if (Erro == "")
            {
                usuarios_tab = await _usuariosService.FirstOrDefault(p => p.USUARIO == usuarios_tab.USUARIO && p.SENHA == usuarios_tab.SENHA);
                if (usuarios_tab != null)
                {
                    List<Claim> claims = new List<Claim>()
                    {
                        new Claim("IdUsuario", usuarios_tab.ID_USUARIO.ToString()),
                        new Claim("CodEmpresa", "0"),
                        new Claim("IdTipoLogin", IdTipoLogin.ToString()),
                    };

                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    AuthenticationProperties properties = new AuthenticationProperties()
                    {
                        AllowRefresh = true,
                        //IsPersistent = modelLogin.KeepLoggedIn
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), properties);

                    return RedirectToAction("Index", "Home");
                }
                else
                    Erro = "Usuário ou senha inválidos";
            }

            ViewBag.Message = Erro;
            CarregaSelectList();
            return View();
        }

        public async void LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
