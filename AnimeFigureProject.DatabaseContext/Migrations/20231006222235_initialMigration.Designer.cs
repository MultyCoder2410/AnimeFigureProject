﻿// <auto-generated />
using System;
using AnimeFigureProject.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AnimeFigureProject.DatabaseContext.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231006222235_initialMigration")]
    partial class initialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AnimeFigureCategory", b =>
                {
                    b.Property<int>("AnimeFiguresId")
                        .HasColumnType("int");

                    b.Property<int>("CategoriesId")
                        .HasColumnType("int");

                    b.HasKey("AnimeFiguresId", "CategoriesId");

                    b.HasIndex("CategoriesId");

                    b.ToTable("AnimeFigureCategory");
                });

            modelBuilder.Entity("AnimeFigureOrigin", b =>
                {
                    b.Property<int>("AnimeFiguresId")
                        .HasColumnType("int");

                    b.Property<int>("OriginsId")
                        .HasColumnType("int");

                    b.HasKey("AnimeFiguresId", "OriginsId");

                    b.HasIndex("OriginsId");

                    b.ToTable("AnimeFigureOrigin");
                });

            modelBuilder.Entity("AnimeFigureProject.EntityModels.AnimeFigure", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("BrandId")
                        .HasColumnType("int");

                    b.Property<int?>("CollectionId")
                        .HasColumnType("int");

                    b.Property<string>("ImageFolderPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<int?>("TypeId")
                        .HasColumnType("int");

                    b.Property<decimal?>("Value")
                        .HasColumnType("decimal(10, 2)");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.HasIndex("CollectionId");

                    b.HasIndex("TypeId");

                    b.ToTable("AnimeFigures");
                });

            modelBuilder.Entity("AnimeFigureProject.EntityModels.Brand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Brands");
                });

            modelBuilder.Entity("AnimeFigureProject.EntityModels.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("AnimeFigureProject.EntityModels.Collection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OwnerId")
                        .HasColumnType("int");

                    b.Property<decimal?>("TotalPrice")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<decimal?>("TotalValue")
                        .HasColumnType("decimal(10, 2)");

                    b.HasKey("Id");

                    b.ToTable("Collections");
                });

            modelBuilder.Entity("AnimeFigureProject.EntityModels.Collector", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Collectors");
                });

            modelBuilder.Entity("AnimeFigureProject.EntityModels.Origin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Origins");
                });

            modelBuilder.Entity("AnimeFigureProject.EntityModels.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AnimeFigureId")
                        .HasColumnType("int");

                    b.Property<string>("OwnerId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AnimeFigureId");

                    b.HasIndex("OwnerId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("AnimeFigureProject.EntityModels.Type", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Types");
                });

            modelBuilder.Entity("CollectionCollector", b =>
                {
                    b.Property<int>("CollectionsId")
                        .HasColumnType("int");

                    b.Property<string>("CollectorsId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CollectionsId", "CollectorsId");

                    b.HasIndex("CollectorsId");

                    b.ToTable("CollectionCollector");
                });

            modelBuilder.Entity("AnimeFigureCategory", b =>
                {
                    b.HasOne("AnimeFigureProject.EntityModels.AnimeFigure", null)
                        .WithMany()
                        .HasForeignKey("AnimeFiguresId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AnimeFigureProject.EntityModels.Category", null)
                        .WithMany()
                        .HasForeignKey("CategoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AnimeFigureOrigin", b =>
                {
                    b.HasOne("AnimeFigureProject.EntityModels.AnimeFigure", null)
                        .WithMany()
                        .HasForeignKey("AnimeFiguresId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AnimeFigureProject.EntityModels.Origin", null)
                        .WithMany()
                        .HasForeignKey("OriginsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AnimeFigureProject.EntityModels.AnimeFigure", b =>
                {
                    b.HasOne("AnimeFigureProject.EntityModels.Brand", "Brand")
                        .WithMany("AnimeFigures")
                        .HasForeignKey("BrandId");

                    b.HasOne("AnimeFigureProject.EntityModels.Collection", null)
                        .WithMany("AnimeFigures")
                        .HasForeignKey("CollectionId");

                    b.HasOne("AnimeFigureProject.EntityModels.Type", "Type")
                        .WithMany("AnimeFigures")
                        .HasForeignKey("TypeId");

                    b.Navigation("Brand");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("AnimeFigureProject.EntityModels.Review", b =>
                {
                    b.HasOne("AnimeFigureProject.EntityModels.AnimeFigure", "AnimeFigure")
                        .WithMany("Reviews")
                        .HasForeignKey("AnimeFigureId");

                    b.HasOne("AnimeFigureProject.EntityModels.Collector", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId");

                    b.Navigation("AnimeFigure");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("CollectionCollector", b =>
                {
                    b.HasOne("AnimeFigureProject.EntityModels.Collection", null)
                        .WithMany()
                        .HasForeignKey("CollectionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AnimeFigureProject.EntityModels.Collector", null)
                        .WithMany()
                        .HasForeignKey("CollectorsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AnimeFigureProject.EntityModels.AnimeFigure", b =>
                {
                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("AnimeFigureProject.EntityModels.Brand", b =>
                {
                    b.Navigation("AnimeFigures");
                });

            modelBuilder.Entity("AnimeFigureProject.EntityModels.Collection", b =>
                {
                    b.Navigation("AnimeFigures");
                });

            modelBuilder.Entity("AnimeFigureProject.EntityModels.Type", b =>
                {
                    b.Navigation("AnimeFigures");
                });
#pragma warning restore 612, 618
        }
    }
}
