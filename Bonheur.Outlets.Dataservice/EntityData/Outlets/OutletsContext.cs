using System;
using System.Collections.Generic;
using Bonheur.Outlets.Dataservice.EntityData.Outlets.Models;
using Microsoft.EntityFrameworkCore;

namespace Bonheur.Outlets.Dataservice.EntityData.Outlets;

public partial class OutletsContext : DbContext
{
    public OutletsContext()
    {
    }

    public string ConnectionString { get; set; }
    public OutletsContext(DbContextOptions<OutletsContext> options, string connectionString)
        : base(options)
    {
        ConnectionString = connectionString;
    }

    public virtual DbSet<Shop> Shops { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;port=3306;database=outlets;user=root;password=asdf1234@#", ServerVersion.Parse("8.0.32-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Shop>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("shops");

            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.ConnectionString)
                .HasMaxLength(255)
                .HasColumnName("Connection_String");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.PhoneNo)
                .HasMaxLength(12)
                .HasColumnName("Phone_no");
            entity.Property(e => e.ShopName)
                .HasMaxLength(110)
                .HasColumnName("Shop_Name");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("users");

            entity.HasIndex(e => e.ShopId, "FK_Users_Shops_ShopId_idx");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(45);
            entity.Property(e => e.Name).HasMaxLength(45);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.PasswordSalt).HasMaxLength(255);
            entity.Property(e => e.PhoneNo)
                .HasMaxLength(12)
                .HasColumnName("Phone_No");
            entity.Property(e => e.ShopId).HasColumnName("Shop_Id");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

            entity.HasOne(d => d.Shop).WithMany(p => p.Users)
                .HasForeignKey(d => d.ShopId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_Shops_ShopId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
