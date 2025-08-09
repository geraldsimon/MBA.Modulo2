using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MBA.Modulo2.Data.Migrations
{
    /// <inheritdoc />
    public partial class EntidadeCliente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Vendedores");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Produtos");

            migrationBuilder.AddColumn<Guid>(
                name: "ClienteId",
                table: "Produtos",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", nullable: true),
                    CriadoEm = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_Customers_ClienteId",
                table: "Produtos");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropIndex(
                name: "IX_Produtos_ClienteId",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "ClienteId",
                table: "Produtos");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Vendedores",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Produtos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
