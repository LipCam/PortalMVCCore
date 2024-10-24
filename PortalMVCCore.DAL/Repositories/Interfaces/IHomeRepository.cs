using PortalMVCCore.DAL.DTOs;

namespace PortalMVCCore.DAL.Repositories.Interfaces
{
    public interface IHomeRepository
    {
        List<DadosDashBoardDTO> GetDashBoard();
    }
}
