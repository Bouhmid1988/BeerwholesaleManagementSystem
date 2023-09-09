﻿// <auto-generated />
using System;
using BeerWholesaleManagementSystem.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BeerWholesaleManagementSystem.Data.Migrations
{
    [DbContext(typeof(BeerDbContext))]
    [Migration("20230908093439_FirstMigration")]
    partial class FirstMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BeerWholesaleManagementSystem.Core.Models.Beer", b =>
                {
                    b.Property<int>("BeerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BeerId"));

                    b.Property<decimal>("AlcoholContent")
                        .HasColumnType("decimal(4, 2)");

                    b.Property<int>("BreweryId")
                        .HasColumnType("int");

                    b.Property<int?>("CommandRequestId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(10, 2)");

                    b.HasKey("BeerId");

                    b.HasIndex("BreweryId");

                    b.HasIndex("CommandRequestId");

                    b.ToTable("Beers");
                });

            modelBuilder.Entity("BeerWholesaleManagementSystem.Core.Models.Brewery", b =>
                {
                    b.Property<int>("BreweryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BreweryId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BreweryId");

                    b.ToTable("Brewery");
                });

            modelBuilder.Entity("BeerWholesaleManagementSystem.Core.Models.CommandRequest", b =>
                {
                    b.Property<int>("CommandRequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CommandRequestId"));

                    b.Property<DateTime>("DateCommande")
                        .HasColumnType("datetime2");

                    b.Property<string>("Statuts")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<int>("WholesalerId")
                        .HasColumnType("int");

                    b.HasKey("CommandRequestId");

                    b.HasIndex("WholesalerId");

                    b.ToTable("CommandeRequest");
                });

            modelBuilder.Entity("BeerWholesaleManagementSystem.Core.Models.SaleBeer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BeerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateSale")
                        .HasColumnType("datetime2");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("WholesalerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WholesalerId");

                    b.ToTable("SaleBeer");
                });

            modelBuilder.Entity("BeerWholesaleManagementSystem.Core.Models.Stock", b =>
                {
                    b.Property<int>("StockId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StockId"));

                    b.Property<int>("BeerId")
                        .HasColumnType("int");

                    b.Property<int>("QuantityStock")
                        .HasColumnType("int");

                    b.Property<int>("WholesalerId")
                        .HasColumnType("int");

                    b.HasKey("StockId");

                    b.HasIndex("BeerId");

                    b.HasIndex("WholesalerId");

                    b.ToTable("Stock");
                });

            modelBuilder.Entity("BeerWholesaleManagementSystem.Core.Models.Wholesaler", b =>
                {
                    b.Property<int>("WholesalerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WholesalerId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("WholesalerId");

                    b.ToTable("Wholesaler");
                });

            modelBuilder.Entity("BeerWholesaleManagementSystem.Core.Models.Beer", b =>
                {
                    b.HasOne("BeerWholesaleManagementSystem.Core.Models.Brewery", "Brewery")
                        .WithMany("Beers")
                        .HasForeignKey("BreweryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BeerWholesaleManagementSystem.Core.Models.CommandRequest", null)
                        .WithMany("BeerCommandees")
                        .HasForeignKey("CommandRequestId");

                    b.Navigation("Brewery");
                });

            modelBuilder.Entity("BeerWholesaleManagementSystem.Core.Models.CommandRequest", b =>
                {
                    b.HasOne("BeerWholesaleManagementSystem.Core.Models.Wholesaler", "Wholesaler")
                        .WithMany("CommandRequest")
                        .HasForeignKey("WholesalerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Wholesaler");
                });

            modelBuilder.Entity("BeerWholesaleManagementSystem.Core.Models.SaleBeer", b =>
                {
                    b.HasOne("BeerWholesaleManagementSystem.Core.Models.Wholesaler", "Wholesaler")
                        .WithMany()
                        .HasForeignKey("WholesalerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Wholesaler");
                });

            modelBuilder.Entity("BeerWholesaleManagementSystem.Core.Models.Stock", b =>
                {
                    b.HasOne("BeerWholesaleManagementSystem.Core.Models.Beer", "Beer")
                        .WithMany("Stocks")
                        .HasForeignKey("BeerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BeerWholesaleManagementSystem.Core.Models.Wholesaler", "wholesaler")
                        .WithMany("Stocks")
                        .HasForeignKey("WholesalerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Beer");

                    b.Navigation("wholesaler");
                });

            modelBuilder.Entity("BeerWholesaleManagementSystem.Core.Models.Beer", b =>
                {
                    b.Navigation("Stocks");
                });

            modelBuilder.Entity("BeerWholesaleManagementSystem.Core.Models.Brewery", b =>
                {
                    b.Navigation("Beers");
                });

            modelBuilder.Entity("BeerWholesaleManagementSystem.Core.Models.CommandRequest", b =>
                {
                    b.Navigation("BeerCommandees");
                });

            modelBuilder.Entity("BeerWholesaleManagementSystem.Core.Models.Wholesaler", b =>
                {
                    b.Navigation("CommandRequest");

                    b.Navigation("Stocks");
                });
#pragma warning restore 612, 618
        }
    }
}
