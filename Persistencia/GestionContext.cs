using Dominio;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistencia
{
    public class GestionContext : IdentityDbContext<Usuarios>
    {
        public GestionContext(DbContextOptions options) :base(options)
        {
                
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            base.OnModelCreating(modelBuilder);
            //mucho a mucho
            //modelBuilder.Entity<GestionContext>().HasKey(ci => new { ci. })
        }

        public DbSet<Clientes> clientes { get; set; }
        public DbSet<Categorias> categorias { get; set; }
        public DbSet<Colores> colores { get; set; }
        public DbSet<CondicionesIva> condicionesIvas { get; set; }
        public DbSet<CuentasCompras> cuentasCompras { get; set; }
        public DbSet<CuentasVentas> cuentasVentas { get; set; }
        public DbSet<EstadosCiviles> ParamEstadosCiviles { get; set; }
        public DbSet<Etiquetas> etiquetas { get; set; }
        public DbSet<Nacionalidades> ParamNacionalidades { get; set; }
        public DbSet<Provincias> ParamProvincias { get; set; }
        public DbSet<TiposDocumentos> ParamTiposDocumentos { get; set; }
        public DbSet<TiposProductos> ParamTiposProductos { get; set; }
        public DbSet<UnidadesMedidas> unidadesMedidas { get; set; }

    }
}
