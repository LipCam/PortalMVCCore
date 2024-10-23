using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PortalMVCCore.DAL.Entities;

[Table("PROGRAMAS_TAB")]
public partial class PROGRAMAS_TAB
{
    [Key]
    public int COD_PROGRAMA { get; set; }

    [StringLength(50)]
    public string? GRUPO_MENU { get; set; }

    [StringLength(100)]
    public string DESCRICAO { get; set; } = null!;

    [StringLength(100)]
    public string? NOME_CONTROLLER { get; set; }

    public string? CAMINHO_STR { get; set; }
}