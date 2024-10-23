using Microsoft.EntityFrameworkCore;
using PortalMVCCore.DAL.Entities;

namespace PortalMVCCore.DAL.DB
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public virtual DbSet<USUARIOS_TAB> USUARIOS_TAB { get; set; }

        public virtual DbSet<PROGRAMAS_TAB> PROGRAMAS_TAB { get; set; }
    }
}
