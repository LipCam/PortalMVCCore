using PortalMVCCore.DAL.DTOs;

namespace PortalMVCCore.BLL.Services.Interfaces
{
    public interface IHomeService
    {
        List<DadosDashBoardDTO> GetDashBoard();
        Task<DadosHeaderDTO> GetDadosHeader(string NomeController, int IdUsuario, int IdTipoLogin);
    }
}
