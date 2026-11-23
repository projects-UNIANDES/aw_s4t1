using Microsoft.EntityFrameworkCore;

namespace aw_s4t1.Models
{
    public class FerreteriaDbContext : DbContext
    {
        public FerreteriaDbContext(DbContextOptions<FerreteriaDbContext> options) : base(options)
        { }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<DetalleVenta> DetallesVenta { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // A category, product or customer with related records cannot be deleted,
            // so sales history is never removed by accident.
            modelBuilder.Entity<Producto>()
                .HasOne(p => p.Categoria).WithMany()
                .HasForeignKey(p => p.CategoriaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Venta>()
                .HasOne(v => v.Cliente).WithMany()
                .HasForeignKey(v => v.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DetalleVenta>()
                .HasOne(d => d.Producto).WithMany()
                .HasForeignKey(d => d.ProductoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Deleting a sale deletes its detail lines.
            modelBuilder.Entity<DetalleVenta>()
                .HasOne(d => d.Venta).WithMany()
                .HasForeignKey(d => d.VentaId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
