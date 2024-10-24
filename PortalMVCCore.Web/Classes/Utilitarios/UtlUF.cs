namespace PortalMVCCore.Web.Classes.Utilitarios
{
    public class UtlUF
    {
        public string UF { get; set; }
        public string Descricao { get; set; }

        public List<UtlUF> GetUFs()
        {
            return new List<UtlUF>()
            {
                new UtlUF() { UF = "AC", Descricao = "Acre" },
                new UtlUF() { UF = "AL", Descricao = "Alagoas" },
                new UtlUF() { UF = "AP", Descricao = "Amapá" },
                new UtlUF() { UF = "AM", Descricao = "Amazonas" },
                new UtlUF() { UF = "BA", Descricao = "Bahia" },
                new UtlUF() { UF = "CE", Descricao = "Ceará" },
                new UtlUF() { UF = "DF", Descricao = "Distrito Federal" },
                new UtlUF() { UF = "ES", Descricao = "Espírito Santo" },
                new UtlUF() { UF = "GO", Descricao = "Goiás" },
                new UtlUF() { UF = "MA", Descricao = "Maranhão" },
                new UtlUF() { UF = "MT", Descricao = "Mato Grosso" },
                new UtlUF() { UF = "MS", Descricao = "Mato Grosso do Sul" },
                new UtlUF() { UF = "MG", Descricao = "Minas Gerais" },
                new UtlUF() { UF = "PA", Descricao = "Pará" },
                new UtlUF() { UF = "PB", Descricao = "Paraíba" },
                new UtlUF() { UF = "PR", Descricao = "Paraná" },
                new UtlUF() { UF = "PE", Descricao = "Pernambuco" },
                new UtlUF() { UF = "PI", Descricao = "Piauí" },
                new UtlUF() { UF = "RJ", Descricao = "Rio de Janeiro" },
                new UtlUF() { UF = "RN", Descricao = "Rio Grande do Norte" },
                new UtlUF() { UF = "RS", Descricao = "Rio Grande do Sul" },
                new UtlUF() { UF = "RO", Descricao = "Rondônia" },
                new UtlUF() { UF = "RR", Descricao = "Roraima" },
                new UtlUF() { UF = "SC", Descricao = "Santa Catarina" },
                new UtlUF() { UF = "SP", Descricao = "São Paulo" },
                new UtlUF() { UF = "SE", Descricao = "Sergipe" },
                new UtlUF() { UF = "TO", Descricao = "Tocantins" },
                };
        }
    }
}
