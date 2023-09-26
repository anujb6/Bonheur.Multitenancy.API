using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Bonheur.Outlets.Dataservice.EntityData.Tenants.Models;

public partial class TenantsContext : DbContext
{
    public TenantsContext()
    {
    }

    public TenantsContext(DbContextOptions<TenantsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Efmigrationshistory> Efmigrationshistories { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<ServiceRecord> ServiceRecords { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("category");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Type).HasMaxLength(110);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("customer");

            entity.HasIndex(e => e.ServiceId, "FK_Customer_Services_ServiceId_idx");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(45);
            entity.Property(e => e.Name).HasMaxLength(110);
            entity.Property(e => e.PhoneNo).HasMaxLength(12);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

            entity.HasOne(d => d.Service).WithMany(p => p.Customers)
                .HasForeignKey(d => d.ServiceId)
                .HasConstraintName("FK_Customer_Services_ServiceId");
        });

        modelBuilder.Entity<Efmigrationshistory>(entity =>
        {
            entity.HasKey(e => e.MigrationId).HasName("PRIMARY");

            entity.ToTable("__efmigrationshistory");

            entity.Property(e => e.MigrationId).HasMaxLength(150);
            entity.Property(e => e.ProductVersion).HasMaxLength(32);
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("services");

            entity.HasIndex(e => e.CategoryId, "FK_Services_Category_CategoryId_idx");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

            entity.HasOne(d => d.Category).WithMany(p => p.Services)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Services_Category_CategoryId");
        });

        modelBuilder.Entity<ServiceRecord>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("service_records");

            entity.HasIndex(e => e.CustomerId, "FK_ServiceRecords_Customer_CustomerId_idx");

            entity.HasIndex(e => e.ServiceId, "FK_ServiceRecords_Services_ServiceId_idx");

            entity.HasOne(d => d.Customer).WithMany(p => p.ServiceRecords)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_ServiceRecords_Customer_CustomerId");

            entity.HasOne(d => d.Service).WithMany(p => p.ServiceRecords)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ServiceRecords_Services_ServiceId");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("staff");

            entity.Property(e => e.Address).HasMaxLength(510);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(110);
            entity.Property(e => e.PhoneNo).HasMaxLength(12);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
