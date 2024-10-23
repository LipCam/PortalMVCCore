using PortalMVCCore.DAL.DB;
using PortalMVCCore.DAL.DTOs;
using PortalMVCCore.DAL.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
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

        public List<DadosDashBoardDTO> GetDashBoard(int IdUsuario, int CodEmpresa, int IdTipoLogin)
        {
            string Erro = "";
            string SQL = @"DASH_BOARD_HOME_PRC @COD_USUARIO, @COD_EMPRESA, @ID_TIPO_LOGIN";
            List<SqlParameter> lst = new List<SqlParameter>()
            {
                new SqlParameter { ParameterName = "@COD_USUARIO", SqlDbType = SqlDbType.Int, Value = IdUsuario },
                new SqlParameter { ParameterName = "@COD_EMPRESA", SqlDbType = SqlDbType.Int, Value = CodEmpresa },
                new SqlParameter { ParameterName = "@ID_TIPO_LOGIN", SqlDbType = SqlDbType.Int, Value = IdTipoLogin },
            };

            DataTable dt = new ExecuteQuery(_configuration).ExecQueryDT(SQL, lst, out Erro);

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
