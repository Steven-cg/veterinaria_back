using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<persona> persona { get; set; }
        public DbSet<mascota> mascota { get; set; }
        public DbSet<usuario> usuario { get; set; }
        public DbSet<producto> producto { get; set; }
        public DbSet<servicio> servicio { get; set; }
        public DbSet<historial_compra> historial_compra { get; set; }
        public DbSet<detalle_servicio> detalle_servicio { get; set; }
        public DbSet<detalle_compra> detalle_compra { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<detalle_compra>()
                .HasOne<historial_compra>()
                .WithMany(h => h.detalle_compra)
                .HasForeignKey(d => d.id_historial_compra)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<detalle_servicio>()
                .HasOne<historial_compra>()
                .WithMany(h => h.detalle_servicio)
                .HasForeignKey(d => d.id_historial_compra)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<detalle_compra>()
                .HasOne<producto>()
                .WithMany()
                .HasForeignKey(d => d.id_producto)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<detalle_servicio>()
                .HasOne<servicio>()
                .WithMany()
                .HasForeignKey(d => d.id_servicio)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
