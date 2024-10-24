using System.Data;

namespace PortalMVCCore.Web.Classes.Utilitarios
{
    public class ConvertDictionary
    {
        public List<Dictionary<string, object>> GetTableRows(DataTable dtData)
        {
            List<Dictionary<string, object>> lstRows = new List<Dictionary<string, object>>();
            Dictionary<string, object>? dictRow = null;

            if (dtData != null)
            {
                foreach (DataRow dr in dtData.Rows)
                {
                    dictRow = new Dictionary<string, object>();
                    foreach (DataColumn col in dtData.Columns)
                    {
                        dictRow.Add(col.ColumnName, dr[col] != DBNull.Value ? dr[col] : "");
                    }
                    lstRows.Add(dictRow);
                }
            }
            return lstRows;
        }
    }
}