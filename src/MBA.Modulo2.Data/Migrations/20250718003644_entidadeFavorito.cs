using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MBA.Modulo2.Data.Migrations
{
    /// <inheritdoc />
    public partial class entidadeFavorito : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Customers_ClienteId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ClienteId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "ClienteId",
                table: "Products");

            migrationBuilder.RenameTable(
                name: "Customers",
                newName: "Clientes");

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Sellers",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Products",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clientes",
                table: "Clientes",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Favoritos",
                columns: table => new
                {
                    ProdutoId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ClienteId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DataAdicao = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favoritos", x => new { x.ClienteId, x.ProdutoId });
                    table.ForeignKey(
                        name: "FK_Favoritos_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Favoritos_Products_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Favoritos_ProdutoId",
                table: "Favoritos",
                column: "ProdutoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Favoritos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Clientes",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "Sellers");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "Products");

            migrationBuilder.RenameTable(
                name: "Clientes",
                newName: "Customers");

            migrationBuilder.AddColumn<Guid>(
                name: "ClienteId",
                table: "Products",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ClienteId",
                table: "Products",
                column: "ClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Customers_ClienteId",
                table: "Products",
                column: "ClienteId",
                principalTable: "Customers",
                principalColumn: "Id");
        }
    }
}
