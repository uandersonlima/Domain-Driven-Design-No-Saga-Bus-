﻿// <auto-generated />
using System;
using AscoreStore.Sales.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AscoreStore.Sales.Data.Migrations
{
    [DbContext(typeof(SalesContext))]
    partial class SalesContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("AscoreStore.Sales.Domain.OrderAggregate.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("Code")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("char(36)");

                    b.Property<decimal>("Discount")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalValue")
                        .HasColumnType("decimal(65,30)");

                    b.Property<Guid?>("VoucherId")
                        .IsRequired()
                        .HasColumnType("char(36)");

                    b.Property<bool>("VoucherUsed")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.HasIndex("VoucherId");

                    b.ToTable("Orders", (string)null);
                });

            modelBuilder.Entity("AscoreStore.Sales.Domain.OrderAggregate.OrderItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("char(36)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("UnitaryValue")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItems", (string)null);
                });

            modelBuilder.Entity("AscoreStore.Sales.Domain.OrderAggregate.Voucher", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DateOfUse")
                        .HasColumnType("datetime(6)");

                    b.Property<decimal?>("DiscountValue")
                        .HasColumnType("decimal(65,30)");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsActivated")
                        .HasColumnType("tinyint(1)");

                    b.Property<decimal?>("Percentage")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<bool>("Used")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("VoucherDiscountType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Vouchers", (string)null);
                });

            modelBuilder.Entity("AscoreStore.Sales.Domain.OrderAggregate.Order", b =>
                {
                    b.HasOne("AscoreStore.Sales.Domain.OrderAggregate.Voucher", "Voucher")
                        .WithMany("Orders")
                        .HasForeignKey("VoucherId")
                        .IsRequired();

                    b.Navigation("Voucher");
                });

            modelBuilder.Entity("AscoreStore.Sales.Domain.OrderAggregate.OrderItem", b =>
                {
                    b.HasOne("AscoreStore.Sales.Domain.OrderAggregate.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("AscoreStore.Sales.Domain.OrderAggregate.Order", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("AscoreStore.Sales.Domain.OrderAggregate.Voucher", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
