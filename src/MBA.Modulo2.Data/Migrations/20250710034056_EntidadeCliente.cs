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
                table: "Sellers");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Products");

            migrationBuilder.AddColumn<Guid>(
                name: "ClienteId",
                table: "Products",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Customers_ClienteId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Products_ClienteId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ClienteId",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Sellers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Products",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
