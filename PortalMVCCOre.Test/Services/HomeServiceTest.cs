using Moq;
using PortalMVCCore.BLL.Services;
using PortalMVCCore.BLL.Services.Interfaces;
using PortalMVCCore.DAL.DTOs;
using PortalMVCCore.DAL.Entities;
using PortalMVCCore.DAL.Repositories.Interfaces;
using System.Linq.Expressions;

namespace PortalMVCCore.Test
{
    public class HomeServiceTest
    {
        [Fact]
        public void GetDashBoard_OK()
        {
            //Arrange
            Mock<IHomeRepository> repository = new Mock<IHomeRepository>();
            IHomeService homeServices = new HomeService(repository.Object, null, null);

            List<DadosDashBoardDTO> lst = new List<DadosDashBoardDTO>()
            {
                new DadosDashBoardDTO() { Descricao = "Clientes", Quantidade = 10, Url = "/Clientes"}
            };

            repository.Setup(p => p.GetDashBoard()).Returns(lst);

            //Act
            List<DadosDashBoardDTO> lstresult = homeServices.GetDashBoard();

            //Assert
            Assert.NotNull(lstresult);
            Assert.Equal(lst[0].Descricao, lstresult[0].Descricao);
        }

        [Fact]
        public async void GetDadosHeader_ObtemDados_OK()
        {
            //Arrange
            string NomeController = "Clientes";
            int IdUsuario = 1, IdTipoLogin = 0;

            Mock<IUsuariosRepository> _usuariosRepositoryMock = new Mock<IUsuariosRepository>();
            _usuariosRepositoryMock.Setup(p => p.FindAsync(IdUsuario)).Returns(Task.FromResult(new USUARIOS_TAB { ID_USUARIO = 1, NOME = "Filipe" }));

            Mock<IProgramasRepository> _programasRepositoryMock = new Mock<IProgramasRepository>();
            _programasRepositoryMock.Setup(p => p.FirstOrDefaultAsync(It.IsAny<Expression<Func<PROGRAMAS_TAB, bool>>>()))
                .Returns(Task.FromResult(new PROGRAMAS_TAB() { NOME_CONTROLLER = "Clientes", DESCRICAO = "Clientes" }));
            //_programasRepositoryMock.Setup(p => p.GetAllAsync(It.IsAny<Expression<Func<PROGRAMAS_TAB, bool>>>()))
            //    .Returns((Expression<Func<PROGRAMAS_TAB, bool>> pred) =>
            //    new List<PROGRAMAS_TAB>()
            //    {
            //        new PROGRAMAS_TAB() { NOME_CONTROLLER = "Usuarios", DESCRICAO = "Usuários" },
            //        new PROGRAMAS_TAB() { NOME_CONTROLLER = "Clientes", DESCRICAO = "Clientes" }
            //    }.AsQueryable().Where(pred).ToList());


            //Act
            DadosHeaderDTO result = await new HomeService(new Mock<IHomeRepository>().Object, _usuariosRepositoryMock.Object, _programasRepositoryMock.Object)
                                            .GetDadosHeader(NomeController, IdUsuario, IdTipoLogin);

            //Accert
            Assert.NotNull(result);
            Assert.Equal(IdUsuario.ToString(), result.IdUsuario);
            Assert.Equal("Filipe", result.Usuario);
            Assert.Equal(NomeController, result.DescricaoController);
            Assert.Equal(IdTipoLogin, result.IdTipoLogin);
        }
    }
}
