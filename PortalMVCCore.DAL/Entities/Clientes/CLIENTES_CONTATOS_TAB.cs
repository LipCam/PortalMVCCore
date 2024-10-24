using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalMVCCore.DAL.Entities.Clientes;

[PrimaryKey("ID_CLIENTE", "ID_CONTATO")]
[Table("CLIENTES_CONTATOS_TAB")]
public partial class CLIENTES_CONTATOS_TAB
{
    [Key]
    public int ID_CLIENTE { get; set; }

    [Key]
    public int ID_CONTATO { get; set; }

    public string? NOME { get; set; }

    public string? FONE { get; set; }

    public string? CELULAR { get; set; }

    public string? EMAIL { get; set; }
}