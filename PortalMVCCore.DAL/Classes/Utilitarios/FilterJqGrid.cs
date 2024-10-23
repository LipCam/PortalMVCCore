using Newtonsoft.Json;

namespace PortalMVCCore.DAL.Classes.Utilitarios
{
    public class FilterJqGrid
    {
        public string groupOp { get; set; }
        public List<RuleJqGrid> rules { get; set; }

        public class RuleJqGrid
        {
            public string field { get; set; }
            public string op { get; set; }
            public string data { get; set; }
        }

        public static FilterJqGrid GetFilterJqGrid(string FilterGrid)
        {
            return JsonConvert.DeserializeObject<FilterJqGrid>(FilterGrid);
        }
    }
}
