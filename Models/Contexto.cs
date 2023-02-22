using Microsoft.EntityFrameworkCore;
using static MVC23.Controllers.VehiculoController;

namespace MVC23.Models
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VehiculoTotal>(
                eb => {
                    eb.HasNoKey();
                });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<MarcaModelo> Marcas { get; set; }

        public DbSet<SerieModelo> Serie { get; set; }

        public DbSet<VehiculoModelo> Vehiculo { get; set; }

        public DbSet<VehiculoTotal> VistaTotal { get; set; }

        public DbSet<ExtraModelo> Extras { get; set; }

        public DbSet<VehiculoExtraModelo> VehiculosExtras { get; set; }

    }
}
