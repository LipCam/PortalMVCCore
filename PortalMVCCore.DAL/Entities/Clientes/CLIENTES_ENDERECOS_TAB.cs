using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalMVCCore.DAL.Entities.Clientes;

[PrimaryKey("ID_CLIENTE", "ID_ENDERECO")]
[Table("CLIENTES_ENDERECOS_TAB")]
public partial class CLIENTES_ENDERECOS_TAB
{
    [Key]
    public int ID_CLIENTE { get; set; }

    [Key]
    public int ID_ENDERECO { get; set; }

    [StringLength(100)]
    public string? ENDERECO { get; set; }    

    [StringLength(50)]
    public string? NUMERO { get; set; }

    [StringLength(100)]
    public string? COMPL { get; set; }

    [StringLength(100)]
    public string? BAIRRO { get; set; }

    [StringLength(100)]
    public string? CIDADE { get; set; }

    [StringLength(2)]
    public string? UF { get; set; }

    [StringLength(20)]
    public string? CEP { get; set; }

    public virtual CLIENTES_TAB COD_CLIENTENavigation { get; set; } = null!;
}