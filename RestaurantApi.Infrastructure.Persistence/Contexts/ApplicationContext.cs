using System;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using RestaurantApi.Core.Domain.Entities;

namespace RestaurantApi.Infrastructure.Persistence.Contexts
{
	public class ApplicationContext : DbContext
    {
        public DbSet<Plato> Platos { get; set; }
        public DbSet<Orden> Ordenes { get; set; }
        public DbSet<Mesa> Mesas { get; set; }
        public DbSet<Ingrediente> Ingredientes { get; set; }
        
        // constructor
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // fluent API
            base.OnModelCreating(modelBuilder);

            #region tables
            modelBuilder.Entity<Plato>().ToTable("Platos");
            modelBuilder.Entity<Orden>().ToTable("Ordenes");
            modelBuilder.Entity<Mesa>().ToTable("Mesas");
            modelBuilder.Entity<Ingrediente>().ToTable("Ingredientes");
            #endregion

            #region PK
            modelBuilder.Entity<Plato>().HasKey(plato => plato.Id);
            modelBuilder.Entity<Orden>().HasKey(orden => orden.Id);
            modelBuilder.Entity<Mesa>().HasKey(mesa => mesa.Id);
            modelBuilder.Entity<Ingrediente>().HasKey(ing => ing.Id);
            #endregion

            #region relationships
            // Orden con Mesa
            modelBuilder.Entity<Orden>()
                .HasOne<Mesa>()
                .WithMany()
                .HasForeignKey(o => o.MesaId)
                .OnDelete(DeleteBehavior.Cascade);

            // Orden con Plato
            modelBuilder.Entity<Orden>()
                .HasMany(o => o.Platos)
                .WithMany(p => p.Ordenes);

            // Plato con Ingrediente
            modelBuilder.Entity<Plato>()
                .HasMany(p => p.Ingredientes)
                .WithMany(i => i.Platos);
            #endregion

            #region property configurations

            #region Mesa
            modelBuilder.Entity<Mesa>(entity =>
            {
                entity.Property(e => e.Capacidad)
                         .IsRequired();

                entity.Property(e => e.Descripcion)
                         .IsRequired()
                         .HasMaxLength(100);

                entity.Property(e => e.Estado)
                        .IsRequired();
            });
            #endregion

            #region Plato
            modelBuilder.Entity<Plato>(entity =>
            {
                entity.Property(e => e.Nombre)
                         .IsRequired()
                         .HasMaxLength(100);

                entity.Property(e => e.Precio)
                         .IsRequired()
                         .HasColumnType("decimal(18,2)");

                entity.Property(e => e.Porcion)
                         .IsRequired();

            });
            #endregion

            #region Orden
            modelBuilder.Entity<Orden>(entity =>
            {
                entity.Property(e => e.Subtotal)
                         .IsRequired()
                         .HasColumnType("decimal(18,2)"); 

                entity.Property(e => e.Descripcion)
                         .HasMaxLength(500); 

            });
            #endregion

            #region Ingrediente
            modelBuilder.Entity<Ingrediente>(entity =>
            {
                entity.Property(e => e.Nombre)
                         .IsRequired()
                         .HasMaxLength(100);
            });
            #endregion

            #endregion
        }

    }
}

