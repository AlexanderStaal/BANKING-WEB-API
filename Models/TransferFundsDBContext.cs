using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BankingWebAPI.Models
{
    public partial class TransferFundsDBContext : DbContext
    {
        public TransferFundsDBContext()
        {
        }

        public TransferFundsDBContext(DbContextOptions<TransferFundsDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TransferFunds> TransferFunds { get; set; }
        public virtual DbSet<TransferFundsStatus> TransferFundsStatus { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Banking");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TransferFunds>(entity =>
            {

                entity.ToTable("TransferFunds");
                entity.Property(e => e.accountFromNumber).HasColumnName("accountFromNumber");
                entity.Property(e => e.accountToNumber).HasColumnName("accountToNumber");
                entity.Property(e => e.amount).HasColumnName("amount");

            });

            modelBuilder.Entity<TransferFundsStatus>(entity =>
            {

                entity.ToTable("TransferFundsStatus");
                entity.Property(e => e.transferFundsStatus).HasColumnName("Return value");

            });


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }


    public partial class TransferSourceDBContext : DbContext
    {
        public TransferSourceDBContext()
        {
        }

        public TransferSourceDBContext(DbContextOptions<TransferSourceDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TransferSource> TransferSource { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Banking");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TransferSource>(entity =>
            {

                entity.ToTable("TransferSource");
                entity.Property(e => e.value).HasColumnName("accountNumber");
                entity.Property(e => e.label).HasColumnName("accountName");

            });


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
