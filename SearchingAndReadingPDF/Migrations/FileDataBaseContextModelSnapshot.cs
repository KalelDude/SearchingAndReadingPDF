﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SearchingAndReadingPDF.Data;

#nullable disable

namespace SearchingAndReadingPDF.Migrations
{
    [DbContext(typeof(FileDataBaseContext))]
    partial class FileDataBaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SearchingAndReadingPDF.Models.PackageValuer", b =>
                {
                    b.Property<int>("PackageValuerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PackageValuerID"));

                    b.Property<string>("ObjectNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PackName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ValuerName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PackageValuerID");

                    b.ToTable("PackageValuer");
                });

            modelBuilder.Entity("SearchingAndReadingPDF.Models.Township", b =>
                {
                    b.Property<int>("TOWNSHIPID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TOWNSHIPID"));

                    b.Property<string>("TOWN_NAME_DESC")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TownshipNames")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TOWNSHIPID");

                    b.ToTable("TownShips");
                });
#pragma warning restore 612, 618
        }
    }
}