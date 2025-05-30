﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MotoRide.Models;

#nullable disable

namespace MotoRide.Migrations
{
    [DbContext(typeof(MotoRideDbContext))]
    [Migration("20250319212736_detsubcatecoryaddby")]
    partial class detsubcatecoryaddby
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MotoRide.Models.Cart", b =>
                {
                    b.Property<int>("CartId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CartId"));

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int?>("OrderId")
                        .HasColumnType("int");

                    b.HasKey("CartId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("OrderId");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("MotoRide.Models.CartItem", b =>
                {
                    b.Property<int>("CartItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CartItemId"));

                    b.Property<int?>("CartId")
                        .HasColumnType("int");

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int?>("MotorcycleId")
                        .HasColumnType("int");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("Size")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CartItemId");

                    b.HasIndex("CartId");

                    b.HasIndex("MotorcycleId");

                    b.HasIndex("ProductId");

                    b.ToTable("CartItems");
                });

            modelBuilder.Entity("MotoRide.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"));

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("MotoRide.Models.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerId"));

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int?>("Level")
                        .HasColumnType("int");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Points")
                        .HasColumnType("int");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomerId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("MotoRide.Models.Motorcycle", b =>
                {
                    b.Property<int>("MotorcycleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MotorcycleId"));

                    b.Property<string>("Brand")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Colors")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Condition")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EngineType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Images")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit");

                    b.Property<decimal?>("Mileage")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("ShopOwnerId")
                        .HasColumnType("int");

                    b.Property<int?>("StockQuantity")
                        .HasColumnType("int");

                    b.Property<int?>("Year")
                        .HasColumnType("int");

                    b.HasKey("MotorcycleId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ShopOwnerId");

                    b.ToTable("Motorcycles");
                });

            modelBuilder.Entity("MotoRide.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"));

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("CustomerNote")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime2");

                    b.Property<float?>("Fee")
                        .HasColumnType("real");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("RecivingDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ShopOwnerId")
                        .HasColumnType("int");

                    b.Property<bool?>("StatusDelivery")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float?>("TotalPrice")
                        .HasColumnType("real");

                    b.HasKey("OrderId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("MotoRide.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"));

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Colors")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Images")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int?>("ShopOwnerId")
                        .HasColumnType("int");

                    b.Property<string>("Sizes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SubCategoryId")
                        .HasColumnType("int");

                    b.HasKey("ProductId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ShopOwnerId");

                    b.HasIndex("SubCategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("MotoRide.Models.Review", b =>
                {
                    b.Property<int>("ReviewId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReviewId"));

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int?>("MotorcycleId")
                        .HasColumnType("int");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.Property<int?>("Rating")
                        .HasColumnType("int");

                    b.HasKey("ReviewId");

                    b.HasIndex("MotorcycleId");

                    b.HasIndex("ProductId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("MotoRide.Models.ShopOwner", b =>
                {
                    b.Property<int>("ShopOwnerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ShopOwnerId"));

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Iamgelicense")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ShopOwnerId");

                    b.ToTable("ShopOwners");
                });

            modelBuilder.Entity("MotoRide.Models.ShopOwnerOrder", b =>
                {
                    b.Property<int>("ShopOwnerOrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ShopOwnerOrderId"));

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int?>("OrderId")
                        .HasColumnType("int");

                    b.Property<int?>("ShopOwnerId")
                        .HasColumnType("int");

                    b.HasKey("ShopOwnerOrderId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ShopOwnerId");

                    b.ToTable("ShopOwnerOrders");
                });

            modelBuilder.Entity("MotoRide.Models.SubCategory", b =>
                {
                    b.Property<int>("SubCategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SubCategoryId"));

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ShopId")
                        .HasColumnType("int");

                    b.HasKey("SubCategoryId");

                    b.ToTable("SubCategories");
                });

            modelBuilder.Entity("MotoRide.Models.WishList", b =>
                {
                    b.Property<int>("WishListId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WishListId"));

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit");

                    b.HasKey("WishListId");

                    b.HasIndex("CustomerId");

                    b.ToTable("WishLists");
                });

            modelBuilder.Entity("MotoRide.Models.WishListItem", b =>
                {
                    b.Property<int>("WishListItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WishListItemId"));

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int?>("MotorcycleId")
                        .HasColumnType("int");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.Property<int?>("WishListId")
                        .HasColumnType("int");

                    b.HasKey("WishListItemId");

                    b.HasIndex("MotorcycleId");

                    b.HasIndex("ProductId");

                    b.HasIndex("WishListId");

                    b.ToTable("WishListItems");
                });

            modelBuilder.Entity("MotoRide.Models.Cart", b =>
                {
                    b.HasOne("MotoRide.Models.Customer", "Customer")
                        .WithMany("Cart")
                        .HasForeignKey("CustomerId");

                    b.HasOne("MotoRide.Models.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId");

                    b.Navigation("Customer");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("MotoRide.Models.CartItem", b =>
                {
                    b.HasOne("MotoRide.Models.Cart", "Cart")
                        .WithMany("CartItem")
                        .HasForeignKey("CartId");

                    b.HasOne("MotoRide.Models.Motorcycle", "Motorcycle")
                        .WithMany()
                        .HasForeignKey("MotorcycleId");

                    b.HasOne("MotoRide.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId");

                    b.Navigation("Cart");

                    b.Navigation("Motorcycle");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("MotoRide.Models.Motorcycle", b =>
                {
                    b.HasOne("MotoRide.Models.Category", "Category")
                        .WithMany("MotorcycleList")
                        .HasForeignKey("CategoryId");

                    b.HasOne("MotoRide.Models.ShopOwner", "ShopOwner")
                        .WithMany()
                        .HasForeignKey("ShopOwnerId");

                    b.Navigation("Category");

                    b.Navigation("ShopOwner");
                });

            modelBuilder.Entity("MotoRide.Models.Order", b =>
                {
                    b.HasOne("MotoRide.Models.Customer", "Customer")
                        .WithMany("Order")
                        .HasForeignKey("CustomerId");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("MotoRide.Models.Product", b =>
                {
                    b.HasOne("MotoRide.Models.Category", "Category")
                        .WithMany("ProductList")
                        .HasForeignKey("CategoryId");

                    b.HasOne("MotoRide.Models.ShopOwner", "ShopOwner")
                        .WithMany()
                        .HasForeignKey("ShopOwnerId");

                    b.HasOne("MotoRide.Models.SubCategory", "SubCategory")
                        .WithMany()
                        .HasForeignKey("SubCategoryId");

                    b.Navigation("Category");

                    b.Navigation("ShopOwner");

                    b.Navigation("SubCategory");
                });

            modelBuilder.Entity("MotoRide.Models.Review", b =>
                {
                    b.HasOne("MotoRide.Models.Motorcycle", "Motorcycle")
                        .WithMany("Reviews")
                        .HasForeignKey("MotorcycleId");

                    b.HasOne("MotoRide.Models.Product", "Product")
                        .WithMany("Reviews")
                        .HasForeignKey("ProductId");

                    b.Navigation("Motorcycle");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("MotoRide.Models.ShopOwnerOrder", b =>
                {
                    b.HasOne("MotoRide.Models.Order", "Order")
                        .WithMany("ShopOwnerOrder")
                        .HasForeignKey("OrderId");

                    b.HasOne("MotoRide.Models.ShopOwner", "ShopOwner")
                        .WithMany()
                        .HasForeignKey("ShopOwnerId");

                    b.Navigation("Order");

                    b.Navigation("ShopOwner");
                });

            modelBuilder.Entity("MotoRide.Models.WishList", b =>
                {
                    b.HasOne("MotoRide.Models.Customer", "Customer")
                        .WithMany("WishList")
                        .HasForeignKey("CustomerId");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("MotoRide.Models.WishListItem", b =>
                {
                    b.HasOne("MotoRide.Models.Motorcycle", "Motorcycle")
                        .WithMany()
                        .HasForeignKey("MotorcycleId");

                    b.HasOne("MotoRide.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId");

                    b.HasOne("MotoRide.Models.WishList", "WishList")
                        .WithMany("WishListItem")
                        .HasForeignKey("WishListId");

                    b.Navigation("Motorcycle");

                    b.Navigation("Product");

                    b.Navigation("WishList");
                });

            modelBuilder.Entity("MotoRide.Models.Cart", b =>
                {
                    b.Navigation("CartItem");
                });

            modelBuilder.Entity("MotoRide.Models.Category", b =>
                {
                    b.Navigation("MotorcycleList");

                    b.Navigation("ProductList");
                });

            modelBuilder.Entity("MotoRide.Models.Customer", b =>
                {
                    b.Navigation("Cart");

                    b.Navigation("Order");

                    b.Navigation("WishList");
                });

            modelBuilder.Entity("MotoRide.Models.Motorcycle", b =>
                {
                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("MotoRide.Models.Order", b =>
                {
                    b.Navigation("ShopOwnerOrder");
                });

            modelBuilder.Entity("MotoRide.Models.Product", b =>
                {
                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("MotoRide.Models.WishList", b =>
                {
                    b.Navigation("WishListItem");
                });
#pragma warning restore 612, 618
        }
    }
}
