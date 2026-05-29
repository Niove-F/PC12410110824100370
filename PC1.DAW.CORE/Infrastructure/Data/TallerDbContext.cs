using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PC1.DAW.CORE.Core.Entities;

namespace PC1.DAW.CORE.Infrastructure.Data;

public partial class TallerDbContext : DbContext
{
    public TallerDbContext()
    {
    }

    public TallerDbContext(DbContextOptions<TallerDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Cliente { get; set; }

    public virtual DbSet<OrdenServicio> OrdenServicio { get; set; }

    public virtual DbSet<TipoServicio> TipoServicio { get; set; }

    public virtual DbSet<Vehiculo> Vehiculo { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=AO2300917;Database=StoreDB;Integrated Security=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.Property(e => e.Nombre).HasMaxLength(50);
            entity.Property(e => e.ApellidoPaterno).HasMaxLength(50);
            entity.Property(e => e.ApellidoMaterno).HasMaxLength(50);
            entity.Property(e => e.Correo).HasMaxLength(50);
            entity.Property(e => e.Telefono).HasMaxLength(50);
        });

        modelBuilder.Entity<OrdenServicio>(entity =>
        {
            entity.Property(e => e.FechaIngreso).HasColumnType("datetime");
            entity.Property(e => e.DescripcionProblema).HasMaxLength(200);
            entity.Property(e => e.CostoEstimado).HasColumnType("float");
            entity.Property(e => e.Estado).HasMaxLength(10);

            entity.HasOne(d => d.Vehiculo).WithMany(p => p.OrdenServicio)
                .HasForeignKey(d => d.ID_V)
                .HasConstraintName("FK_Favorite_Product");

            entity.HasOne(d => d.User).WithMany(p => p.Favorite)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Favorite_User");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Orders).WithMany(p => p.OrderDetail)
                .HasForeignKey(d => d.OrdersId)
                .HasConstraintName("FK_OrderDetail_Orders");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetail)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_OrderDetail_Product");
        });

        modelBuilder.Entity<Orders>(entity =>
        {
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Orders_User");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Orders).WithMany(p => p.Payment)
                .HasForeignKey(d => d.OrdersId)
                .HasConstraintName("FK_Payment_Orders");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.Description).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Category).WithMany(p => p.Product)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_Product_Category");
        });

        modelBuilder.Entity<ProductDetail>(entity =>
        {
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductDetail)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_ProductDetail_Product");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.Country).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.Type)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
