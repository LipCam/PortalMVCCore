using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using PortalMVCCore.DAL.Classes.Utilitarios;
using static PortalMVCCore.DAL.Classes.Utilitarios.FilterJqGrid;

namespace PortalMVCCore.DAL.DB
{
    public class ExecuteQuery
    {
        IConfiguration _configuration;

        public ExecuteQuery(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DataTable ExecQueryDT(string SQL, List<SqlParameter> lstSqlParameter, out string Erro, string FilterGrid = "")
        {
            Erro = "";
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            DataTable dt = new DataTable();
            SqlDataAdapter adp = new SqlDataAdapter("", con);

            if (lstSqlParameter != null)
            {
                foreach (var item in lstSqlParameter)
                    adp.SelectCommand.Parameters.Add(item.ParameterName, item.SqlDbType).Value = item.Value;
            }

            if (!string.IsNullOrEmpty(FilterGrid))
            {
                FilterJqGrid JsonFilterGrid = FilterJqGrid.GetFilterJqGrid(FilterGrid);
                List<string> lstCondicoes = new List<string>();

                foreach (RuleJqGrid item in JsonFilterGrid.rules)
                {
                    string tipo = item.field.Substring(item.field.Length - 3);

                    switch (tipo.ToUpper())
                    {
                        case "STR":
                            {
                                lstCondicoes.Add(item.field + " LIKE '%' + @" + item.field + " + '%'");
                                adp.SelectCommand.Parameters.Add("@" + item.field, SqlDbType.VarChar).Value = item.data;
                                break;
                            }
                        default:
                            {
                                lstCondicoes.Add(item.field + " LIKE '%' + @" + item.field + " + '%'");
                                adp.SelectCommand.Parameters.Add("@" + item.field, SqlDbType.VarChar).Value = item.data;
                                break;
                            }
                    }
                }

                if (lstCondicoes.Count > 0)
                {
                    if (SQL.ToUpper().IndexOf("WHERE") == -1)
                        SQL += " WHERE";
                    else
                        SQL += " AND";

                    SQL += " " + string.Join(" AND ", lstCondicoes);
                }
            }

            try
            {
                adp.SelectCommand.CommandText = SQL;
                adp.Fill(dt);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    Erro = ex.InnerException.Message;
                else
                    Erro = ex.Message;
            }

            return dt;
        }

        public DataSet ExecQueryDS(string SQL, List<SqlParameter> lstSqlParameter, out string Erro)
        {
            Erro = "";
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            DataSet ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter(SQL, con);

            if (lstSqlParameter != null)
            {
                foreach (var item in lstSqlParameter)
                    adp.SelectCommand.Parameters.Add(item.ParameterName, item.SqlDbType).Value = item.Value;
            }

            try
            {
                adp.Fill(ds);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    Erro = ex.InnerException.Message;
                else
                    Erro = ex.Message;
            }

            return ds;
        }
    }
}