﻿// <auto-generated />
using BankingWebAPI.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BankingAPI.Migrations
{
    [DbContext(typeof(AccountDBContext))]
    partial class AccountDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BankingWebAPI.Models.Accounts", b =>
                {
                    b.Property<int>("accountNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("AccountNumber");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("accountNumber"), 1L, 1);

                    b.Property<string>("accountName")
                        .IsRequired()
                        .HasMaxLength(15)
                        .IsUnicode(false)
                        .HasColumnType("varchar(15)")
                        .HasColumnName("AccountName");

                    b.Property<double>("balance")
                        .HasMaxLength(32)
                        .IsUnicode(false)
                        .HasColumnType("float")
                        .HasColumnName("Balance");

                    b.HasKey("accountNumber");

                    b.ToTable("Account", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
