using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_commercial_Web_RESTAPI.Migrations
{
    /// <inheritdoc />
    public partial class ModifyingPaymentCustomerTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "payments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "payments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "payments");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "payments");
        }
    }
}
