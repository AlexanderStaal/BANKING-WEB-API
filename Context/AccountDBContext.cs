using System;
using BankingWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BankingWebAPI.Context
{
    public partial class AccountDBContext : DbContext
    {
        public AccountDBContext()
        {
        }

        public AccountDBContext(DbContextOptions<AccountDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Accounts> Accounts { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Banking");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Accounts>(entity =>
            {

                entity.ToTable("Account");

                entity.Property(e => e.accountNumber).HasColumnName("AccountNumber");

                entity.Property(e => e.accountName)
                    .HasColumnName("AccountName")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.balance)
                    .HasColumnName("Balance")
                    .HasMaxLength(32)
                    .IsUnicode(false);

            });


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }

    public partial class CreteAccountDBContext : DbContext
    {
        public CreteAccountDBContext()
        {
        }

        public CreteAccountDBContext(DbContextOptions<CreteAccountDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CreateAccount> ReturnValue { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Banking");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CreateAccount>(entity =>
            {
                entity.ToTable("CreateAccount");
                entity.Property(e => e.ReturnValue).HasColumnName("Return Value");
            });


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
