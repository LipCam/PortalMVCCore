using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using PortalMVCCore.BLL.Services.Interfaces;
using PortalMVCCore.Controllers;
using PortalMVCCore.DAL.Entities;
using System.Linq.Expressions;

namespace PortalMVCCore.Test.Controllers
{
    public class LoginControllerTest
    {
        [Fact]
        public async void Login_ok()
        {
            Mock<IAuthenticationService> _mockAuthenticationService = new Mock<IAuthenticationService>(); //await HttpContext.SignInAsync
            Mock<IUrlHelper> _mockUrlHelper = new(); //return RedirectToAction("Index", "Home");
            Mock<IUsuariosService> _usuariosService = new Mock<IUsuariosService>();

            // Configura o HttpContext com o AuthenticationService mockado
            var httpContext = new DefaultHttpContext();
            httpContext.RequestServices = new ServiceCollection()
                .AddSingleton(_mockAuthenticationService.Object)
                .BuildServiceProvider();

            // Configura o UrlHelper mockado para evitar erros de redirecionamento
            _mockUrlHelper.Setup(x => x.Action(It.IsAny<UrlActionContext>())).Returns("fakeUrl");

            LoginController _controller = new LoginController(_usuariosService.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = httpContext
                },
                Url = _mockUrlHelper.Object
            };


            //Arrange
            USUARIOS_TAB usuarios_tab = new USUARIOS_TAB() { USUARIO = "admin", SENHA = "admin" };

            _usuariosService.Setup(p => p.FirstOrDefault(It.IsAny<Expression<Func<USUARIOS_TAB, bool>>>()))
                .Returns(Task.FromResult(usuarios_tab));


            //Act
            var result = await _controller.Index(usuarios_tab, 0);


            //Assert
            RedirectToActionResult redirectToActionResult = result as RedirectToActionResult;
            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.Equal("Home", redirectToActionResult.ControllerName);
        }

        [Fact]
        public async void Login_fail()
        {
            Mock<IUsuariosService> _usuariosService = new Mock<IUsuariosService>();
            LoginController _controller = new LoginController(_usuariosService.Object);

            //Arrange
            USUARIOS_TAB usuarios_tab = new USUARIOS_TAB() { USUARIO = "admin", SENHA = "admin" };

            _usuariosService.Setup(p => p.FirstOrDefault(It.IsAny<Expression<Func<USUARIOS_TAB, bool>>>()))
                .Returns(Task.FromResult((USUARIOS_TAB)null));


            //Act
            var result = await _controller.Index(usuarios_tab, 0);

            //Assert
            ViewResult viewResult = result as ViewResult;
            Assert.Equal("Usuário ou senha inválidos", viewResult.ViewData["Message"]);
        }


    }
}