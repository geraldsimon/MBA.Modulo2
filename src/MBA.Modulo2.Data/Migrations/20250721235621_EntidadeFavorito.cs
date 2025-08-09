using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MBA.Modulo2.Data.Migrations
{
    /// <inheritdoc />
    public partial class EntidadeFavorito : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_Clientes_ClienteId",
                table: "Produtos");

            migrationBuilder.DropIndex(
                name: "IX_Produtos_ClienteId",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "ClienteId",
                table: "Produtos");

            migrationBuilder.AlterColumn<bool>(
                name: "Ativo",
                table: "Vendedores",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INT");

            migrationBuilder.AlterColumn<bool>(
                name: "Ativo",
                table: "Produtos",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INT");

            migrationBuilder.CreateTable(
                name: "Favoritos",
                columns: table => new
                {
                    ClienteId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProdutoId = table.Column<Guid>(type: "TEXT", nullable: false),
                    AdicionadoEm = table.Column<DateTime>(type: "TEXT", nullable: false)
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
                        name: "FK_Favoritos_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produtos",
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

            migrationBuilder.AlterColumn<int>(
                name: "Ativo",
                table: "Vendedores",
                type: "INT",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "Ativo",
                table: "Produtos",
                type: "INT",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<Guid>(
                name: "ClienteId",
                table: "Produtos",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_ClienteId",
                table: "Produtos",
                column: "ClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_Clientes_ClienteId",
                table: "Produtos",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id");
        }
    }
}
