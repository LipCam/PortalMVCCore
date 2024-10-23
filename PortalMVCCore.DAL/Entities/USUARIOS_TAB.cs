using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalMVCCore.DAL.Entities;

[Table("USUARIOS_TAB")]
public partial class USUARIOS_TAB
{
    [Key]
    public int COD_USUARIO { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? USUARIO { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? NOME { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? SENHA { get; set; }
}