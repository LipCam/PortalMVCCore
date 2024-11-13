namespace PortalMVCCore.DAL.DTOs
{
    public class DadosHeaderDTO
    {
        public string? IdUsuario { get; set; }
        public string? Usuario { get; set; }
        public string? Empresa { get; set; }
        public string? NomeController { get; set; }
        public string? DescricaoController { get; set; }
        public int IdTipoLogin { get; set; }
        public int IntervNotific { get; set; }
    }
}
