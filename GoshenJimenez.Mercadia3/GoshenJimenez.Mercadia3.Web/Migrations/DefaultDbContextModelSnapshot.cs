﻿// <auto-generated />
using System;
using GoshenJimenez.Mercadia3.Web.Infrastructure.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GoshenJimenez.Mercadia3.Web.Migrations
{
    [DbContext(typeof(DefaultDbContext))]
    partial class DefaultDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("GoshenJimenez.Mercadia3.Web.Infrastructure.Domain.Models.Category", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("TagLine")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("GoshenJimenez.Mercadia3.Web.Infrastructure.Domain.Models.Product", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("CategoryId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("TagLine")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("GoshenJimenez.Mercadia3.Web.Infrastructure.Domain.Models.ProductTag", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid?>("ProductId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("TagId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("TagId");

                    b.ToTable("ProductTags");
                });

            modelBuilder.Entity("GoshenJimenez.Mercadia3.Web.Infrastructure.Domain.Models.Tag", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Tags");

                    b.HasData(
                        new
                        {
                            Id = new Guid("32febb22-f596-4b1e-b0a8-b11ad54be200"),
                            CreatedAt = new DateTime(2021, 1, 28, 1, 50, 50, 559, DateTimeKind.Utc).AddTicks(5627),
                            Name = "Tag 1",
                            UpdatedAt = new DateTime(2021, 1, 28, 1, 50, 50, 559, DateTimeKind.Utc).AddTicks(6846)
                        },
                        new
                        {
                            Id = new Guid("32febb22-f596-4b1e-b0a8-b11ad54be201"),
                            CreatedAt = new DateTime(2021, 1, 28, 1, 50, 50, 560, DateTimeKind.Utc).AddTicks(565),
                            Name = "Tag 2",
                            UpdatedAt = new DateTime(2021, 1, 28, 1, 50, 50, 560, DateTimeKind.Utc).AddTicks(594)
                        },
                        new
                        {
                            Id = new Guid("32febb22-f596-4b1e-b0a8-b11ad54be203"),
                            CreatedAt = new DateTime(2021, 1, 28, 1, 50, 50, 560, DateTimeKind.Utc).AddTicks(662),
                            Name = "Tag 3",
                            UpdatedAt = new DateTime(2021, 1, 28, 1, 50, 50, 560, DateTimeKind.Utc).AddTicks(665)
                        });
                });

            modelBuilder.Entity("GoshenJimenez.Mercadia3.Web.Infrastructure.Domain.Models.Product", b =>
                {
                    b.HasOne("GoshenJimenez.Mercadia3.Web.Infrastructure.Domain.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");
                });

            modelBuilder.Entity("GoshenJimenez.Mercadia3.Web.Infrastructure.Domain.Models.ProductTag", b =>
                {
                    b.HasOne("GoshenJimenez.Mercadia3.Web.Infrastructure.Domain.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId");

                    b.HasOne("GoshenJimenez.Mercadia3.Web.Infrastructure.Domain.Models.Tag", "Tag")
                        .WithMany()
                        .HasForeignKey("TagId");
                });
#pragma warning restore 612, 618
        }
    }
}
