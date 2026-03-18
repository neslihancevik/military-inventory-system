using Microsoft.EntityFrameworkCore;
using MilitaryInventory.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace MilitaryInventory.Data
{
    public class MilitaryDbContext : DbContext
    {
        public MilitaryDbContext(DbContextOptions<MilitaryDbContext> options)
            : base(options) { }

        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<EquipmentType> EquipmentTypes { get; set; }
        public DbSet<Personnel> Personnel { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Assignment>()
                .HasOne(a => a.Equipment)
                .WithMany(e => e.Assignments)
                .HasForeignKey(a => a.EquipmentId);

            modelBuilder.Entity<Assignment>()
                .HasOne(a => a.Personnel)
                .WithMany(p => p.Assignments)
                .HasForeignKey(a => a.PersonnelId);

            // DbContext içindeki ilgili kısmı şu şekilde değiştir:
            modelBuilder.Entity<Assignment>()
                .HasOne(a => a.AssignedByNavigation) // a.AssignedByUser yerine bunu yazdık
                .WithMany(u => u.Assignments)
                .HasForeignKey(a => a.AssignedBy);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId);

            modelBuilder.Entity<AuditLog>()
    .HasOne(a => a.User)
    .WithMany() // Eğer User sınıfında ICollection<AuditLog> yoksa burayı boş bırakabilirsin
    .HasForeignKey(a => a.UserId);
        }
    }
}
