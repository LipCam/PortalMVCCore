using Microsoft.AspNetCore.Mvc;
using Moq;
using PortalMVCCore.BLL.Services.Interfaces;
using PortalMVCCore.DAL.Entities;
using PortalMVCCore.Web.Controllers;

namespace PortalMVCCore.Test.Controllers
{
    public class UsuariosControllerTest
    {
        [Fact]
        public async void UpdateUsuario_OK()
        {
            Mock<IUsuariosService> _usuariosService = new Mock<IUsuariosService>();
            UsuariosController _controller = new UsuariosController(_usuariosService.Object);

            //Arrange
            USUARIOS_TAB usuarios_tab = new USUARIOS_TAB() { ID_USUARIO = 1 };
            _usuariosService.Setup(p => p.Update(usuarios_tab));

            //Act
            var result = _controller.frmUsuarios(1, usuarios_tab, false);

            //Assert
            RedirectToActionResult? redirectToActionResult = await result as RedirectToActionResult;
            Assert.Equal("frmUsuarios", redirectToActionResult.ActionName);
            Assert.Equal(1, redirectToActionResult.RouteValues["Id"]);
        }

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