using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalMVCCore.DAL.Entities.Clientes;

[Table("CLIENTES_TAB")]
public partial class CLIENTES_TAB
{
    [Key]
    public int ID_CLIENTE { get; set; }

    [StringLength(50)]
    public string? CODIGO { get; set; }

    [StringLength(50)]
    public string? NOME { get; set; }

    [StringLength(50)]
    public string? FANTASIA { get; set; }

    public int? TIPO_PESSOA { get; set; }

    [StringLength(20)]
    public string? CPF { get; set; }

    [StringLength(20)]
    public string? CNPJ { get; set; }

    [StringLength(20)]
    public string? FONE { get; set; }

    [StringLength(20)]
    public string? CELULAR { get; set; }

    public virtual ICollection<CLIENTES_ENDERECOS_TAB> CLIENTES_ENDERECOS_TABs { get; set; } = new List<CLIENTES_ENDERECOS_TAB>();
}