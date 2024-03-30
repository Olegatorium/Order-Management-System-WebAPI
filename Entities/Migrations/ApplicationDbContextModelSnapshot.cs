﻿// <auto-generated />
using System;
using DataBaseContent;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataBaseContent.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DataBaseContent.Order", b =>
                {
                    b.Property<Guid>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("OrderNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("OrderId");

                    b.ToTable("Orders", (string)null);

                    b.HasData(
                        new
                        {
                            OrderId = new Guid("39b5e8ef-0f83-450f-a037-83720976999f"),
                            CustomerName = "John Doe",
                            OrderDate = new DateTime(2024, 3, 27, 3, 35, 39, 504, DateTimeKind.Utc).AddTicks(8768),
                            OrderNumber = "Order_2024_1",
                            TotalAmount = 1777.00m
                        });
                });

            modelBuilder.Entity("DataBaseContent.OrderItem", b =>
                {
                    b.Property<Guid>("OrderItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("OrderItemId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItems", (string)null);

                    b.HasData(
                        new
                        {
                            OrderItemId = new Guid("04942d7c-4d8a-49ea-8f78-f3b9fb8e0764"),
                            OrderId = new Guid("39b5e8ef-0f83-450f-a037-83720976999f"),
                            ProductName = "Iphone 14",
                            Quantity = 2,
                            UnitPrice = 785.00m
                        },
                        new
                        {
                            OrderItemId = new Guid("3e1d04b2-2aba-43bd-9d6b-ed383abd8771"),
                            OrderId = new Guid("39b5e8ef-0f83-450f-a037-83720976999f"),
                            ProductName = "Microwave",
                            Quantity = 3,
                            UnitPrice = 69.00m
                        });
                });

            modelBuilder.Entity("DataBaseContent.OrderItem", b =>
                {
                    b.HasOne("DataBaseContent.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });
#pragma warning restore 612, 618
        }
    }
}
