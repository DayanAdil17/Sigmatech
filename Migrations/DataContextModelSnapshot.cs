﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Sigmatech.Infrastructure.Context;

#nullable disable

namespace Sigmatech.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("src.Entities.Menu.MenuEntity", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("createdAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("createdBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("deletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("deletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isAvailable")
                        .HasColumnType("bit");

                    b.Property<string>("menuCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("menuName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("menuType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("price")
                        .HasColumnType("int");

                    b.Property<DateTime?>("updatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("updatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Menus");
                });

            modelBuilder.Entity("src.Entities.Transaction.TransactionEntity", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("createdAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("createdBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("deletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("deletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isActive")
                        .HasColumnType("bit");

                    b.Property<bool>("isPaid")
                        .HasColumnType("bit");

                    b.Property<string>("pic")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("totalPrice")
                        .HasColumnType("int");

                    b.Property<string>("transactionNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("updatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("updatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("src.Entities.User.UserEntity", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("createdAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("createdBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("deletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("deletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("updatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("updatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("userName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("userRole")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("src.Entities.Transaction.TransactionEntity", b =>
                {
                    b.OwnsMany("src.Entities.Transaction.DetailTransaction", "detailTransactions", b1 =>
                        {
                            b1.Property<Guid>("TransactionEntityid")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            b1.Property<string>("menuCode")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("menuName")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("menuType")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<int>("price")
                                .HasColumnType("int");

                            b1.Property<int>("quantity")
                                .HasColumnType("int");

                            b1.HasKey("TransactionEntityid", "Id");

                            b1.ToTable("Transactions");

                            b1.ToJson("detailTransactions");

                            b1.WithOwner()
                                .HasForeignKey("TransactionEntityid");
                        });

                    b.Navigation("detailTransactions");
                });
#pragma warning restore 612, 618
        }
    }
}
