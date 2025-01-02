using Moq;
using PortalMVCCore.BLL.Services;
using PortalMVCCore.BLL.Services.Interfaces;
using PortalMVCCore.DAL.Entities.Clientes;
using PortalMVCCore.DAL.Repositories.Interfaces;

namespace PortalMVCCore.Test.Services
{
    public class ClientesServiceTest
    {
        private Mock<IClientesRepository> _clientesRepositoryMock;
        private IClientesService _clientesService;


        public ClientesServiceTest()
        {
            _clientesRepositoryMock = new Mock<IClientesRepository>();
            _clientesService = new ClientesService(_clientesRepositoryMock.Object);
        }

        [Fact]
        public async void GetAllClientes_ShouldGet2()
        {
            //Arrange
            _clientesRepositoryMock.Setup(p => p.GetAllAsync(null))
                .ReturnsAsync(new List<CLIENTES_TAB>()
                {
                    new CLIENTES_TAB() { NOME = "cliente 1" },
                    new CLIENTES_TAB() { NOME = "cliente 2" }
                });

            //Act
            var result = await _clientesService.GetAll();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async void AddCliente_ShouldAdd()
        {
            //Arrange
            CLIENTES_TAB clientes_tab = new CLIENTES_TAB() { ID_CLIENTE = 3, NOME = "cliente 1" };

            _clientesRepositoryMock.Setup(p => p.GetAllAsync(null))
                .ReturnsAsync(new List<CLIENTES_TAB>()
                {
                    new CLIENTES_TAB() { ID_CLIENTE = 1, NOME = "cliente 1" },
                    new CLIENTES_TAB() { ID_CLIENTE = 2, NOME = "cliente 2" }
                });

            _clientesRepositoryMock.Setup(p => p.AddAsync(clientes_tab))
                .ReturnsAsync(clientes_tab);

            //Act
            var result = await _clientesService.Add(clientes_tab);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(clientes_tab.ID_CLIENTE, 3);
            Assert.Equal(clientes_tab.NOME, result.NOME);
        }

        [Fact]
        public async void UpdateCliente_ShouldUpdate()
        {
            //Arrange
            CLIENTES_TAB CLIENTES_TAB = new CLIENTES_TAB() { NOME = "cliente 1" };

            _clientesRepositoryMock.Setup(p => p.UpdateAsync(CLIENTES_TAB));

            //Act
            await _clientesService.Update(CLIENTES_TAB);

            //Assert
            _clientesRepositoryMock.Verify(repo => repo.UpdateAsync(It.Is<CLIENTES_TAB>(u => u.NOME == "cliente 1")), Times.Once);
        }
    }
}
