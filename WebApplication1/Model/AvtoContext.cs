using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Model;

public partial class AvtoContext : DbContext
{
    public AvtoContext()
    {
    }

    public AvtoContext(DbContextOptions<AvtoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Purchase> Purchases { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;user=root;password=2511;database=avto", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.35-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Purchase>(entity =>
        {
            entity.HasKey(e => e.Idpurchase).HasName("PRIMARY");

            entity.ToTable("purchase");

            entity.HasIndex(e => e.IdUser, "use_idx");

            entity.Property(e => e.Idpurchase)
                .ValueGeneratedNever()
                .HasColumnName("idpurchase");
            entity.Property(e => e.Datetime).HasColumnType("datetime");
            entity.Property(e => e.IdUser).HasColumnName("idUser");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Purchases)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("use");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUsers).HasName("PRIMARY");

            entity.ToTable("user");

            entity.HasIndex(e => e.Login, "Login_UNIQUE").IsUnique();

            entity.Property(e => e.IdUsers).HasColumnName("idUsers");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.RoleId).HasMaxLength(255);
            entity.Property(e => e.Salt).HasMaxLength(255);
            entity.Property(e => e.Token).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
