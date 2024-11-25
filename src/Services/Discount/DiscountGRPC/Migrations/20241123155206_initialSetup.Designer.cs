﻿// <auto-generated />
using DiscountGRPC.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DiscountGRPC.Migrations
{
    [DbContext(typeof(DiscountContext))]
    [Migration("20241123155206_initialSetup")]
    partial class initialSetup
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("DiscountGRPC.Models.Coupons", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Amount")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProductDescription")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Coupon");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amount = 100m,
                            ProductDescription = "iphone black friday offer",
                            ProductName = "Iphone 14 Pro"
                        },
                        new
                        {
                            Id = 2,
                            Amount = 80m,
                            ProductDescription = "samsung black friday offer",
                            ProductName = "Samsung 14"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
