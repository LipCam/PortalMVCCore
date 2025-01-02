using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PortalMVCCore.BLL.Services.Interfaces;
using PortalMVCCore.DAL.DTOs;
using PortalMVCCore.DAL.Entities;
using PortalMVCCore.Web.Controllers;
using System.Security.Claims;

namespace PortalMVCCore.Test.Controllers
{
    public class HomeControllerTest
    {
        [Fact]
        public async void GetDadosHeader_HomeController_Ok()
        {
            Mock<IHomeService> _mock = new Mock<IHomeService>();

            HomeController _controller = new HomeController(_mock.Object);

            // Arrange
            const int idUsuario = 1;
            const int idTipoLogin = 0;
            const string nomeUsuario = "Usuario";
            const string nomeController = "Clientes";
            const string descricaoPrograma = "Clientes";

            var claims = new List<Claim>
            {
                new Claim("IdUsuario", idUsuario.ToString()),
                new Claim("IdTipoLogin", idTipoLogin.ToString())
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var principal = new ClaimsPrincipal(identity);
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = principal }
            };

            // Configuração do mock de _usuariosRepository
            _mock.Setup(p => p.GetDadosHeader(nomeController, idUsuario, idTipoLogin))
            //_mock.Setup(p => p.GetDadosHeader(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                                   .Returns(Task.FromResult(new DadosHeaderDTO { IdUsuario = "1", Usuario = "Usuario", IdTipoLogin = 0, DescricaoController = "Clientes" }));

            // Act
            var result = await _controller.GetDadosHeader(nomeController) as JsonResult;

            // Assert
            Assert.NotNull(result);
            var dados = result.Value as DadosHeaderDTO;
            Assert.NotNull(dados);
            Assert.Equal(idUsuario.ToString(), dados.IdUsuario);
            Assert.Equal(idTipoLogin, dados.IdTipoLogin);
            Assert.Equal(nomeUsuario, dados.Usuario);
            Assert.Equal(descricaoPrograma, dados.DescricaoController);
        }

        //Exemplo antigo de teste no HomeController do método GetDadosHeader q era assim:
        /*public IActionResult GetDadosHeader(string NomeController = "")
        {
            int.TryParse(User.FindFirst("IdUsuario")?.Value, out int IdUsuario);
            int.TryParse(User.FindFirst("IdTipoLogin")?.Value, out int IdTipoLogin);

            DadosHeaderDTO Dados = new DadosHeaderDTO();
            Dados.IdUsuario = IdUsuario.ToString();
            Dados.Usuario = "";

            USUARIOS_TAB usuarios_tab = _usuariosRepository.Find(IdUsuario);

            if (usuarios_tab != null)
                Dados.Usuario = usuarios_tab.NOME.Count() <= 26 ? usuarios_tab.NOME : usuarios_tab.NOME.Substring(0, 26);

            PROGRAMAS_TAB programas_tab = _programasRepository.GetAll(p => p.NOME_CONTROLLER == NomeController).FirstOrDefault();
            Dados.DescricaoController = programas_tab != null ? programas_tab.DESCRICAO : "";

            Dados.IdTipoLogin = IdTipoLogin;

            return Json(Dados);
        }*/

        /*[Fact]
        public void GetDadosHeader_RetornaDadosHeaderCorretamente()
        {
            Mock<IHomeService> _mockHomeService = new Mock<IHomeService>();
            Mock<IUsuariosRepository> _mockUsuariosRepository = new Mock<IUsuariosRepository>();
            Mock<IProgramasRepository> _mockProgramasRepository = new Mock<IProgramasRepository>();
            HomeController _controller = new HomeController(_mockHomeService.Object, _mockUsuariosRepository.Object, _mockProgramasRepository.Object);

            // Arrange
            const int idUsuario = 1;
            const int idTipoLogin = 2;
            const string nomeUsuario = "Nome Completo do Usuario";
            const string nomeController = "ControllerTeste";
            const string descricaoPrograma = "Descrição do Programa";

            var claims = new List<Claim>
            {
                new Claim("IdUsuario", idUsuario.ToString()),
                new Claim("IdTipoLogin", idTipoLogin.ToString())
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var principal = new ClaimsPrincipal(identity);
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = principal }
            };

            // Configuração do mock de _usuariosRepository
            _mockUsuariosRepository.Setup(repo => repo.Find(It.IsAny<int>()))
                                   .Returns(new USUARIOS_TAB { NOME = nomeUsuario });

            // Configuração do mock de _programasRepository
            //_mockProgramasRepository.Setup(repo => repo.GetAll(It.IsAny<Expression<Func<PROGRAMAS_TAB, bool>>>()))
            //                        .Returns(new List<PROGRAMAS_TAB>
            //                            {
            //                                //new PROGRAMAS_TAB { NOME_CONTROLLER = nomeController, DESCRICAO = descricaoPrograma },
            //                                new PROGRAMAS_TAB { NOME_CONTROLLER = "Empresas", DESCRICAO = "Empresas" },
            //                                new PROGRAMAS_TAB { NOME_CONTROLLER = "Clientes", DESCRICAO = "Clientes" },
            //                            }.ToList());

            _mockProgramasRepository.Setup(repo => repo.GetAll(It.IsAny<Expression<Func<PROGRAMAS_TAB, bool>>>()))
                .Returns((Expression<Func<PROGRAMAS_TAB, bool>> predicate) =>
                    new List<PROGRAMAS_TAB>
                    {
                        new PROGRAMAS_TAB { NOME_CONTROLLER = nomeController, DESCRICAO = descricaoPrograma },
                        new PROGRAMAS_TAB { NOME_CONTROLLER = "Empresas", DESCRICAO = "Empresas" },
                        new PROGRAMAS_TAB { NOME_CONTROLLER = "Clientes", DESCRICAO = "Clientes" }
                    }.AsQueryable().Where(predicate).ToList());


            // Act
            var result = _controller.GetDadosHeader(nomeController) as JsonResult;

            // Assert
            Assert.NotNull(result);
            var dados = result.Value as DadosHeaderDTO;
            Assert.NotNull(dados);
            Assert.Equal(idUsuario.ToString(), dados.IdUsuario);
            Assert.Equal(idTipoLogin, dados.IdTipoLogin);
            Assert.Equal(descricaoPrograma, dados.DescricaoController);
            Assert.Equal(nomeUsuario, dados.Usuario);
        }*/



        //[Fact]
        //public async void Login_ok()
        //{
        //    Mock<IAuthenticationService> _mockAuthenticationService = new Mock<IAuthenticationService>(); //await HttpContext.SignInAsync
        //    Mock<IUrlHelper> _mockUrlHelper = new Mock<IUrlHelper>(); //return RedirectToAction("Index", "Home");
        //    Mock<IUsuariosRepository> _usuariosRepository = new Mock<IUsuariosRepository>();

        //    // Configura o HttpContext com o AuthenticationService mockado
        //    var httpContext = new DefaultHttpContext();
        //    httpContext.RequestServices = new ServiceCollection()
        //        .AddSingleton(_mockAuthenticationService.Object)
        //        .BuildServiceProvider();

        //    // Configura o UrlHelper mockado para evitar erros de redirecionamento
        //    _mockUrlHelper.Setup(x => x.Action(It.IsAny<UrlActionContext>())).Returns("fakeUrl");

        //    LoginController _controller = new LoginController(_usuariosRepository.Object)
        //    {
        //        ControllerContext = new ControllerContext()
        //        {
        //            HttpContext = httpContext
        //        },
        //        Url = _mockUrlHelper.Object
        //    };


        //    //Arrange
        //    USUARIOS_TAB usuarios_tab = new USUARIOS_TAB() { USUARIO = "admin", SENHA = "admin" };

        //    _usuariosRepository.Setup(p => p.FirstOrDefault(It.IsAny<Expression<Func<USUARIOS_TAB, bool>>>()))
        //        .Returns(usuarios_tab);


        //    //Act
        //    var result = await _controller.Index(usuarios_tab, 0);


        //    //Assert
        //    RedirectToActionResult redirectToActionResult = result as RedirectToActionResult;
        //    Assert.Equal("Index", redirectToActionResult.ActionName);
        //    Assert.Equal("Home", redirectToActionResult.ControllerName);
        //}

        //[Fact]
        //public async void Login_fail()
        //{            
        //    Mock<IUsuariosRepository> _usuariosRepository = new Mock<IUsuariosRepository>();
        //    LoginController _controller = new LoginController(_usuariosRepository.Object);

        //    //Arrange
        //    USUARIOS_TAB usuarios_tab = new USUARIOS_TAB() { USUARIO = "admin", SENHA = "admin" };

        //    _usuariosRepository.Setup(p => p.FirstOrDefault(It.IsAny<Expression<Func<USUARIOS_TAB, bool>>>()))
        //        .Returns((USUARIOS_TAB)null);


        //    //Act
        //    var result = await _controller.Index(usuarios_tab, 0);

        //    //Assert
        //    ViewResult viewResult = result as ViewResult;
        //    Assert.Equal("Usuário ou senha inválidos", viewResult.ViewData["Message"]);
        //}
    }
}