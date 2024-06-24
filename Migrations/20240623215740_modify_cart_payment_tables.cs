using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace E_commercial_Web_RESTAPI.Migrations
{
    /// <inheritdoc />
    public partial class modify_cart_payment_tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "carts",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "carts",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "carts");

            migrationBuilder.DropColumn(
                name: "sources",
                table: "carts");

            migrationBuilder.AddColumn<long>(
                name: "amount",
                table: "carts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "amount",
                table: "carts");

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "carts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "sources",
                table: "carts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "carts",
                columns: new[] { "Id", "Currency", "customerId", "sources" },
                values: new object[,]
                {
                    { 1L, "USD", 1L, "tok_visa" },
                    { 2L, "EUR", 2L, "tok_mastercard" }
                });
        }
    }
}
