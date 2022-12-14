using System;
using BankingWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BankingWebAPI.Context
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
                entity.Property(e => e.fromAccountNumber).HasColumnName("fromAccountNumber");
                entity.Property(e => e.toAccountNumber).HasColumnName("toAccountNumber");
                entity.Property(e => e.transactionTime).HasColumnName("transactionTime");
                entity.Property(e => e.fromAccountBalance).HasColumnName("fromAccountBalance");
                entity.Property(e => e.toAccountBalance).HasColumnName("toAccountBalance");
            });
            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
