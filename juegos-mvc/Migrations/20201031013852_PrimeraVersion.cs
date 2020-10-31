using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace juegos_mvc.Migrations
{
    public partial class PrimeraVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Administradores",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nombre = table.Column<string>(maxLength: 100, nullable: false),
                    Apellido = table.Column<string>(maxLength: 100, nullable: false),
                    FechaAlta = table.Column<DateTime>(nullable: false),
                    FechaUltimaModificacion = table.Column<DateTime>(nullable: true),
                    FechaUltimoAcceso = table.Column<DateTime>(nullable: true),
                    Username = table.Column<string>(maxLength: 50, nullable: false),
                    Password = table.Column<byte[]>(nullable: true),
                    Legajo = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administradores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Descripcion = table.Column<string>(maxLength: 200, nullable: false),
                    Css = table.Column<string>(maxLength: 200, nullable: true),
                    PorcentajeDescuento = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nombre = table.Column<string>(maxLength: 100, nullable: false),
                    Apellido = table.Column<string>(maxLength: 100, nullable: false),
                    FechaAlta = table.Column<DateTime>(nullable: false),
                    FechaUltimaModificacion = table.Column<DateTime>(nullable: true),
                    FechaUltimoAcceso = table.Column<DateTime>(nullable: true),
                    Username = table.Column<string>(maxLength: 50, nullable: false),
                    Password = table.Column<byte[]>(nullable: true),
                    Dni = table.Column<string>(maxLength: 20, nullable: false),
                    FechaDeNacimiento = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Consolas",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Descripcion = table.Column<string>(maxLength: 200, nullable: false),
                    IconoCss = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consolas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Generos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Descripcion = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Generos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Juegos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Titulo = table.Column<string>(maxLength: 200, nullable: false),
                    Stock = table.Column<int>(nullable: false),
                    PrecioOriginal = table.Column<decimal>(nullable: false),
                    AnioLanzamiento = table.Column<int>(nullable: false),
                    CategoriaId = table.Column<Guid>(nullable: false),
                    ConsolaId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Juegos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Juegos_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Juegos_Consolas_ConsolaId",
                        column: x => x.ConsolaId,
                        principalTable: "Consolas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Compras",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    JuegoId = table.Column<Guid>(nullable: false),
                    ClienteId = table.Column<Guid>(nullable: false),
                    FechaCompra = table.Column<DateTime>(nullable: false),
                    PrecioOriginal = table.Column<decimal>(nullable: false),
                    PrecioFinal = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Compras_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Compras_Juegos_JuegoId",
                        column: x => x.JuegoId,
                        principalTable: "Juegos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JuegoGeneros",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    GeneroId = table.Column<Guid>(nullable: false),
                    JuegoId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JuegoGeneros", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JuegoGeneros_Generos_GeneroId",
                        column: x => x.GeneroId,
                        principalTable: "Generos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JuegoGeneros_Juegos_JuegoId",
                        column: x => x.JuegoId,
                        principalTable: "Juegos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Compras_ClienteId",
                table: "Compras",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Compras_JuegoId",
                table: "Compras",
                column: "JuegoId");

            migrationBuilder.CreateIndex(
                name: "IX_JuegoGeneros_GeneroId",
                table: "JuegoGeneros",
                column: "GeneroId");

            migrationBuilder.CreateIndex(
                name: "IX_JuegoGeneros_JuegoId",
                table: "JuegoGeneros",
                column: "JuegoId");

            migrationBuilder.CreateIndex(
                name: "IX_Juegos_CategoriaId",
                table: "Juegos",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Juegos_ConsolaId",
                table: "Juegos",
                column: "ConsolaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Administradores");

            migrationBuilder.DropTable(
                name: "Compras");

            migrationBuilder.DropTable(
                name: "JuegoGeneros");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Generos");

            migrationBuilder.DropTable(
                name: "Juegos");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "Consolas");
        }
    }
}
