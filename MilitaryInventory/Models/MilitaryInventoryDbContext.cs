using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MilitaryInventory.Models;

public partial class MilitaryInventoryDbContext : DbContext
{
    public MilitaryInventoryDbContext()
    {
    }

    public MilitaryInventoryDbContext(DbContextOptions<MilitaryInventoryDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Assignment> Assignments { get; set; }

    public virtual DbSet<AuditLog> AuditLogs { get; set; }

    public virtual DbSet<Equipment> Equipment { get; set; }

    public virtual DbSet<EquipmentType> EquipmentTypes { get; set; }

    public virtual DbSet<Personnel> Personnel { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=MilitaryInventoryDB;User Id=neslihan23;Password=Sn121323;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Assignment>(entity =>
        {
            entity.HasKey(e => e.AssignmentId).HasName("PK__Assignme__32499E7784E0BE92");

            entity.Property(e => e.AssignmentId).ValueGeneratedNever();
            entity.Property(e => e.AssignedDate).HasColumnType("datetime");
            entity.Property(e => e.ReturnedDate).HasColumnType("datetime");

            entity.HasOne(d => d.AssignedByNavigation).WithMany(p => p.Assignments)
                .HasForeignKey(d => d.AssignedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Assign_User");

            entity.HasOne(d => d.Equipment).WithMany(p => p.Assignments)
                .HasForeignKey(d => d.EquipmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Assign_Equipment");

            entity.HasOne(d => d.Personnel).WithMany(p => p.Assignments)
                .HasForeignKey(d => d.PersonnelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Assign_Personnel");
        });

        modelBuilder.Entity<AuditLog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__AuditLog__5E54864854AC1914");

            entity.Property(e => e.Action).HasMaxLength(100);
            entity.Property(e => e.ActionDate).HasColumnType("datetime");
            entity.Property(e => e.Ipaddress)
                .HasMaxLength(50)
                .HasColumnName("IPAddress");
            entity.Property(e => e.RecordId).HasMaxLength(100);
            entity.Property(e => e.TableName).HasMaxLength(50);
        });

        modelBuilder.Entity<Equipment>(entity =>
        {
            entity.HasKey(e => e.EquipmentId).HasName("PK__Equipmen__34474479145309C1");

            entity.HasIndex(e => e.SerialNumber, "UQ__Equipmen__048A000818C119FC").IsUnique();

            entity.Property(e => e.EquipmentId).ValueGeneratedNever();
            entity.Property(e => e.EquipmentName).HasMaxLength(100);
            entity.Property(e => e.SerialNumber).HasMaxLength(100);

            entity.HasOne(d => d.EquipmentType).WithMany(p => p.Equipment)
                .HasForeignKey(d => d.EquipmentTypeId)
                .HasConstraintName("FK_Equipment_Types");
        });

        modelBuilder.Entity<EquipmentType>(entity =>
        {
            entity.HasKey(e => e.EquipmentTypeId).HasName("PK__Equipmen__554726FC03586AE9");

            entity.Property(e => e.TypeName).HasMaxLength(50);
        });

        modelBuilder.Entity<Personnel>(entity =>
        {
            entity.HasKey(e => e.PersonnelId).HasName("PK__Personne__CAFBCB4F84CB92F4");

            entity.Property(e => e.PersonnelId).ValueGeneratedNever();
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Rank).HasMaxLength(50);
            entity.Property(e => e.Unit).HasMaxLength(100);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE1A060A5A7D");

            entity.HasIndex(e => e.RoleName, "UQ__Roles__8A2B6160EFA7B54F").IsUnique();

            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C371DD36A");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D105346FDBC7F6").IsUnique();

            entity.Property(e => e.UserId).ValueGeneratedNever();
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_Roles");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
