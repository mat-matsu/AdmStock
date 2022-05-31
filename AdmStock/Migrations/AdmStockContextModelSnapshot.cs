﻿// <auto-generated />
using System;
using AdmStock.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AdmStock.Migrations
{
    [DbContext(typeof(AdmStockContext))]
    partial class AdmStockContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("AdmStock.Models.Articulo", b =>
                {
                    b.Property<int>("art_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("art_id"), 1L, 1);

                    b.Property<string>("tipo_prod")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("art_id");

                    b.ToTable("Articulos");
                });

            modelBuilder.Entity("AdmStock.Models.Cliente", b =>
                {
                    b.Property<int>("cliente_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("cliente_id"), 1L, 1);

                    b.Property<string>("cliente_dir")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("cliente_dni")
                        .HasColumnType("int");

                    b.Property<string>("cliente_nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("cliente_tel")
                        .HasColumnType("int");

                    b.HasKey("cliente_id");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("AdmStock.Models.Lote", b =>
                {
                    b.Property<int>("lote_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("lote_id"), 1L, 1);

                    b.Property<int>("lote_cant")
                        .HasColumnType("int");

                    b.Property<double>("lote_precio")
                        .HasColumnType("float");

                    b.Property<int>("prod_id")
                        .HasColumnType("int");

                    b.Property<int>("prov_id")
                        .HasColumnType("int");

                    b.HasKey("lote_id");

                    b.HasIndex("prod_id");

                    b.HasIndex("prov_id");

                    b.ToTable("Lotes");
                });

            modelBuilder.Entity("AdmStock.Models.Producto", b =>
                {
                    b.Property<int>("prod_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("prod_id"), 1L, 1);

                    b.Property<int>("art_id")
                        .HasColumnType("int");

                    b.Property<string>("prod_desc")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("prod_nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("prod_id");

                    b.HasIndex("art_id");

                    b.ToTable("Productos");
                });

            modelBuilder.Entity("AdmStock.Models.Proveedor", b =>
                {
                    b.Property<int>("prov_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("prov_id"), 1L, 1);

                    b.Property<string>("prov_cuil")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("prov_dir")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("prov_nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("prov_tel")
                        .HasColumnType("int");

                    b.HasKey("prov_id");

                    b.ToTable("Proveedores");
                });

            modelBuilder.Entity("AdmStock.Models.Venta", b =>
                {
                    b.Property<int>("venta_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("venta_id"), 1L, 1);

                    b.Property<int>("cliente_id")
                        .HasColumnType("int");

                    b.Property<int>("lote_id")
                        .HasColumnType("int");

                    b.Property<int>("venta_cant")
                        .HasColumnType("int");

                    b.Property<DateTime>("venta_fecha")
                        .HasColumnType("datetime2");

                    b.HasKey("venta_id");

                    b.HasIndex("cliente_id");

                    b.HasIndex("lote_id");

                    b.ToTable("Ventas");
                });

            modelBuilder.Entity("AdmStock.Models.Lote", b =>
                {
                    b.HasOne("AdmStock.Models.Producto", "Productos")
                        .WithMany()
                        .HasForeignKey("prod_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AdmStock.Models.Proveedor", "Proveedores")
                        .WithMany()
                        .HasForeignKey("prov_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Productos");

                    b.Navigation("Proveedores");
                });

            modelBuilder.Entity("AdmStock.Models.Producto", b =>
                {
                    b.HasOne("AdmStock.Models.Articulo", "Articulos")
                        .WithMany()
                        .HasForeignKey("art_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Articulos");
                });

            modelBuilder.Entity("AdmStock.Models.Venta", b =>
                {
                    b.HasOne("AdmStock.Models.Cliente", "Clientes")
                        .WithMany()
                        .HasForeignKey("cliente_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AdmStock.Models.Lote", "Lote")
                        .WithMany()
                        .HasForeignKey("lote_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Clientes");

                    b.Navigation("Lote");
                });
#pragma warning restore 612, 618
        }
    }
}
