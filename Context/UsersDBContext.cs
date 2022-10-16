using System;
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
            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
