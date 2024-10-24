using Microsoft.EntityFrameworkCore;
using PortalMVCCore.DAL.Entities;
using PortalMVCCore.DAL.Entities.Clientes;

namespace PortalMVCCore.DAL.DB
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public virtual DbSet<USUARIOS_TAB> USUARIOS_TAB { get; set; }

        public virtual DbSet<PROGRAMAS_TAB> PROGRAMAS_TAB { get; set; }

        public virtual DbSet<CLIENTES_TAB> CLIENTES_TAB { get; set; }
        public virtual DbSet<CLIENTES_ENDERECOS_TAB> CLIENTES_ENDERECOS_TAB { get; set; }
        public virtual DbSet<CLIENTES_CONTATOS_TAB> CLIENTES_CONTATOS_TAB { get; set; }        
    }
}
