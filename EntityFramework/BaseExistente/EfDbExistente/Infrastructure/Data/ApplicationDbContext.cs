using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EfDbExistente.Infrastructure.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<Produto> Produtos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Categorias_Id");

            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Produto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Produtos_Id");

            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Preco).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Quantidade).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Categoria).WithMany(p => p.Produtos)
                .HasForeignKey(d => d.CategoriaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Produtos_Categorias");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
