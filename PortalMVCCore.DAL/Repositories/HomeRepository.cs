using Microsoft.Extensions.Configuration;
using PortalMVCCore.DAL.DB;
using PortalMVCCore.DAL.DTOs;
using PortalMVCCore.DAL.Repositories.Interfaces;
using System.Data;

namespace PortalMVCCore.DAL.Repositories
{
    public class HomeRepository : IHomeRepository
    {
        private IConfiguration _configuration;

        public HomeRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<DadosDashBoardDTO> GetDashBoard()
        {
            string Erro = "";
            string SQL = @"EXEC DASH_BOARD_HOME_PRC";

            DataTable dt = new ExecuteQuery(_configuration).ExecQueryDT(SQL, null, out Erro);

            List<DadosDashBoardDTO> lstDadosDashBoardDTO = new List<DadosDashBoardDTO>();
            foreach (DataRow row in dt.Rows)
            {
                lstDadosDashBoardDTO.Add(new DadosDashBoardDTO()
                {
                    Descricao = row["DESCRICAO"].ToString(),
                    Quantidade = Convert.ToInt32(row["QUANTIDADE"]),
                    Url = row["URL"].ToString(),
                });
            }

            return lstDadosDashBoardDTO;
        }
    }
}
