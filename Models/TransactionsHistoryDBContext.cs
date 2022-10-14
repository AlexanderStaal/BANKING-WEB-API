using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BankingWebAPI.Models
{
    public partial class TransactionsHistoryDBContext : DbContext
    {
        public TransactionsHistoryDBContext()
        {
        }

        public TransactionsHistoryDBContext(DbContextOptions<TransactionsHistoryDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TransactionsHistory> GetAllTransactions { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Banking");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TransactionsHistory>(entity =>
            {
                entity.ToTable("TransactionsHistory");
                entity.Property(e => e.transactionId).HasColumnName("transactionId");
                entity.Property(e => e.accountFromNumber).HasColumnName("accountFromNumber");
                entity.Property(e => e.accountToNumber).HasColumnName("accountToNumber");
                entity.Property(e => e.transactionTime).HasColumnName("transactionTime");
                entity.Property(e => e.fromAccountBalance).HasColumnName("fromAccountBalance");
                entity.Property(e => e.toAccountBalance).HasColumnName("toAccountBalance");
            });
            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
