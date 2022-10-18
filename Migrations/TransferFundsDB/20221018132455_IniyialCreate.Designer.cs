﻿// <auto-generated />
using BankingWebAPI.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BankingAPI.Migrations.TransferFundsDB
{
    [DbContext(typeof(TransferFundsDBContext))]
    [Migration("20221018132455_IniyialCreate")]
    partial class IniyialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BankingWebAPI.Models.TransferFunds", b =>
                {
                    b.Property<int>("fromAccountNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("fromAccountNumber");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("fromAccountNumber"), 1L, 1);

                    b.Property<double>("amount")
                        .HasColumnType("float")
                        .HasColumnName("amount");

                    b.Property<int>("toAccountNumber")
                        .HasColumnType("int")
                        .HasColumnName("toAccountNumber");

                    b.HasKey("fromAccountNumber");

                    b.ToTable("TransferFunds", (string)null);
                });

            modelBuilder.Entity("BankingWebAPI.Models.TransferFundsStatus", b =>
                {
                    b.Property<string>("transferFundsStatus")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("Return value");

                    b.HasKey("transferFundsStatus");

                    b.ToTable("TransferFundsStatus", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
