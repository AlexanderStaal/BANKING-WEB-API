using System;
using BankingWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;


namespace BankingWebAPI.Context
{
    public partial class UsersDBContext : DbContext
    {
        public UsersDBContext()
        {
        }

        public UsersDBContext(DbContextOptions<UsersDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Refreshtoken> Refreshtoken { get; set; }
        public virtual DbSet<Permission> Permission { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<Role> Role { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Banking");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.Property(e => e.firstName).HasColumnName("FirstName");
                entity.Property(e => e.lastName).HasColumnName("LastName");
                entity.Property(e => e.userName).HasColumnName("UserName");
                entity.Property(e => e.role).HasColumnName("Role");
                entity.Property(e => e.password).HasColumnName("Password");
                entity.Property(e => e.token).HasColumnName("Token");
                entity.Property(e => e.email).HasColumnName("Email");
            });


            modelBuilder.Entity<Refreshtoken>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("Refreshtoken");

                entity.Property(e => e.UserId)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.TokenId)
                    .HasMaxLength(32)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.HasKey(e => new { e.RoleId, e.MenuId });

                entity.ToTable("Permission");

                entity.Property(e => e.RoleId)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.MenuId)
                    .HasMaxLength(32)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.ToTable("Menu");

                entity.Property(e => e.Id)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.LinkName)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(32)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.Roleid);

                entity.ToTable("Role");

                entity.Property(e => e.Roleid)
                    .HasColumnName("roleid")
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(32)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
