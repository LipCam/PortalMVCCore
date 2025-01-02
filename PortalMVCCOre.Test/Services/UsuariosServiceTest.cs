using Moq;
using PortalMVCCore.BLL.Services;
using PortalMVCCore.BLL.Services.Interfaces;
using PortalMVCCore.DAL.Entities;
using PortalMVCCore.DAL.Repositories.Interfaces;

namespace PortalMVCCore.Test.Services
{
    public class UsuariosServiceTest
    {
        private Mock<IUsuariosRepository> _usuariosRepositoryMock;
        private IUsuariosService _usuariosService;


        public UsuariosServiceTest()
        {
            _usuariosRepositoryMock = new Mock<IUsuariosRepository>();
            _usuariosService = new UsuariosService(_usuariosRepositoryMock.Object);
        }

        [Fact]
        public async void GetUsuarioByLogin_Senha_OK()
        {
            //Arrange
            string Usuario = "usuario";
            string Senha = "123";

            _usuariosRepositoryMock.Setup(p => p.FirstOrDefaultAsync(p => p.USUARIO == Usuario && p.SENHA == Senha))
                .ReturnsAsync(new USUARIOS_TAB() { USUARIO = "usuario", SENHA = "123" });

            //Act
            var result = await _usuariosService.FirstOrDefault(p => p.USUARIO == Usuario && p.SENHA == Senha);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(Usuario, result.USUARIO);
        }

        [Fact]
        public async void GetUsuarioByLogin_Senha_Fail()
        {
            //Arrange
            string Usuario = "usuario";
            string Senha = "123";

            _usuariosRepositoryMock.Setup(p => p.FirstOrDefaultAsync(p => p.USUARIO == Usuario && p.SENHA == Senha))
                .ReturnsAsync((USUARIOS_TAB)null);

            //Act
            var result = await _usuariosService.FirstOrDefault(p => p.USUARIO == Usuario && p.SENHA == Senha);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async void GetAllUsers_ShouldGet2()
        {
            //Arrange
            _usuariosRepositoryMock.Setup(p => p.GetAllAsync(null))
                .ReturnsAsync(new List<USUARIOS_TAB>()
                {
                    new USUARIOS_TAB() { NOME = "usuario 1" },
                    new USUARIOS_TAB() { NOME = "usuario 2" }
                });

            //Act
            var result = await _usuariosService.GetAll();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async void AddUser_ShouldAdd()
        {
            //Arrange
            USUARIOS_TAB usuarios_tab = new USUARIOS_TAB() { NOME = "usuario 1" };

            _usuariosRepositoryMock.Setup(p => p.GetAllAsync(null))
                .ReturnsAsync(new List<USUARIOS_TAB>()
                {
                    new USUARIOS_TAB() { ID_USUARIO = 1, NOME = "usuario 1" },
                    new USUARIOS_TAB() { ID_USUARIO = 2, NOME = "usuario 2" }
                });

            _usuariosRepositoryMock.Setup(p => p.AddAsync(usuarios_tab))
                .ReturnsAsync(usuarios_tab);

            //Act
            var result = await _usuariosService.Add(usuarios_tab);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(usuarios_tab.ID_USUARIO, 3);
            Assert.Equal(usuarios_tab.NOME, result.NOME);
        }

        [Fact]
        public async void UpdateUser_ShouldUpdate()
        {
            //Arrange
            USUARIOS_TAB usuarios_tab = new USUARIOS_TAB() { NOME = "usuario 1" };

            _usuariosRepositoryMock.Setup(p => p.UpdateAsync(usuarios_tab));

            //Act
            await _usuariosService.Update(usuarios_tab);

            //Assert
            _usuariosRepositoryMock.Verify(repo => repo.UpdateAsync(It.Is<USUARIOS_TAB>(u => u.NOME == "usuario 1")), Times.Once);
        }
    }
}
