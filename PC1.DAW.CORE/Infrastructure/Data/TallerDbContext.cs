using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PC1.DAW.CORE.Core.Entities;

namespace PC1.DAW.CORE.Infrastructure.Data
{
    public partial class TallerDbContext : DbContext
    {
        public TallerDbContext()
        {
        }

        public TallerDbContext(DbContextOptions<TallerDbContext> options)
            : base(options)
        {
        }

        // =========================
        // DBSets
        // =========================

        public virtual DbSet<Cliente> Cliente { get; set; }

        public virtual DbSet<Vehiculo> Vehiculo { get; set; }

        public virtual DbSet<OrdenServicio> OrdenServicio { get; set; }

        public virtual DbSet<TipoServicio> TipoServicio { get; set; }

        // =========================
        // CONNECTION
        // =========================

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning Move your connection string out of source code for security.
            => optionsBuilder.UseSqlServer(
                "Server=localhost;Database=TallerMec;Integrated Security=True;TrustServerCertificate=True");

        // =========================
        // MODEL CONFIGURATION
        // =========================

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // =========================
            // CLIENTE
            // =========================
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.ID);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50);

                entity.Property(e => e.ApellidoPaterno)
                    .HasMaxLength(50);

                entity.Property(e => e.ApellidoMaterno)
                    .HasMaxLength(50);

                entity.Property(e => e.Correo)
                    .HasMaxLength(50);

                entity.Property(e => e.Telefono)
                    .HasMaxLength(20);
            });

            // =========================
            // VEHICULO
            // =========================
            modelBuilder.Entity<Vehiculo>(entity =>
            {
                entity.HasKey(e => e.ID_V);

                entity.Property(e => e.Placa)
                    .HasMaxLength(20);

                entity.Property(e => e.Marca)
                    .HasMaxLength(50);

                entity.Property(e => e.Modelo)
                    .HasMaxLength(50);

                entity.Property(e => e.Año);

                // Relación:
                // Cliente 1 ---- N Vehiculo
                entity.HasOne(d => d.Cliente)
                    .WithMany(p => p.Vehiculos)
                    .HasForeignKey(d => d.ID_Cliente)
                    .HasConstraintName("FK_Vehiculo_Cliente");
            });

            // =========================
            // TIPO SERVICIO
            // =========================
            modelBuilder.Entity<TipoServicio>(entity =>
            {
                entity.HasKey(e => e.ID_TS);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50);

                entity.Property(e => e.PrecioBase)
                    .HasColumnType("decimal(10,2)");
            });

            // =========================
            // ORDEN SERVICIO
            // =========================
            modelBuilder.Entity<OrdenServicio>(entity =>
            {
                entity.HasKey(e => e.ID_OS);

                entity.Property(e => e.FechaIngreso)
                    .HasColumnType("datetime");

                entity.Property(e => e.DescripcionProblema)
                    .HasMaxLength(200);

                entity.Property(e => e.CostoEstimado)
                    .HasColumnType("decimal(10,2)");

                entity.Property(e => e.Estado)
                    .HasMaxLength(20);

                // Relación:
                // Vehiculo 1 ---- N OrdenServicio
                entity.HasOne(d => d.Vehiculo)
                    .WithMany(p => p.OrdenServicios)
                    .HasForeignKey(d => d.ID_V)
                    .HasConstraintName("FK_OrdenServicio_Vehiculo");

                // Relación:
                // TipoServicio 1 ---- N OrdenServicio
                entity.HasOne(d => d.TipoServicio)
                    .WithMany(p => p.OrdenServicios)
                    .HasForeignKey(d => d.ID_TS)
                    .HasConstraintName("FK_OrdenServicio_TipoServicio");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}