using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdmStock.Migrations
{
    public partial class firstCommit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Articulos",
                columns: table => new
                {
                    art_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tipo_prod = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articulos", x => x.art_id);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    cliente_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cliente_nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cliente_dni = table.Column<int>(type: "int", nullable: false),
                    cliente_dir = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cliente_tel = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.cliente_id);
                });

            migrationBuilder.CreateTable(
                name: "Proveedores",
                columns: table => new
                {
                    prov_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    prov_cuil = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    prov_nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    prov_dir = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    prov_tel = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedores", x => x.prov_id);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    prod_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    art_id = table.Column<int>(type: "int", nullable: false),
                    prod_nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    prod_desc = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.prod_id);
                    table.ForeignKey(
                        name: "FK_Productos_Articulos_art_id",
                        column: x => x.art_id,
                        principalTable: "Articulos",
                        principalColumn: "art_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lotes",
                columns: table => new
                {
                    lote_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    prod_id = table.Column<int>(type: "int", nullable: false),
                    prov_id = table.Column<int>(type: "int", nullable: false),
                    lote_cant = table.Column<int>(type: "int", nullable: false),
                    lote_precio = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lotes", x => x.lote_id);
                    table.ForeignKey(
                        name: "FK_Lotes_Productos_prod_id",
                        column: x => x.prod_id,
                        principalTable: "Productos",
                        principalColumn: "prod_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lotes_Proveedores_prov_id",
                        column: x => x.prov_id,
                        principalTable: "Proveedores",
                        principalColumn: "prov_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ventas",
                columns: table => new
                {
                    venta_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    lote_id = table.Column<int>(type: "int", nullable: false),
                    cliente_id = table.Column<int>(type: "int", nullable: false),
                    venta_cant = table.Column<int>(type: "int", nullable: false),
                    venta_fecha = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ventas", x => x.venta_id);
                    table.ForeignKey(
                        name: "FK_Ventas_Clientes_cliente_id",
                        column: x => x.cliente_id,
                        principalTable: "Clientes",
                        principalColumn: "cliente_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ventas_Lotes_lote_id",
                        column: x => x.lote_id,
                        principalTable: "Lotes",
                        principalColumn: "lote_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lotes_prod_id",
                table: "Lotes",
                column: "prod_id");

            migrationBuilder.CreateIndex(
                name: "IX_Lotes_prov_id",
                table: "Lotes",
                column: "prov_id");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_art_id",
                table: "Productos",
                column: "art_id");

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_cliente_id",
                table: "Ventas",
                column: "cliente_id");

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_lote_id",
                table: "Ventas",
                column: "lote_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ventas");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Lotes");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Proveedores");

            migrationBuilder.DropTable(
                name: "Articulos");
        }
    }
}
