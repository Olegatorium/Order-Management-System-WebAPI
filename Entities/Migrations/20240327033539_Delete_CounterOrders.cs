using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataBaseContent.Migrations
{
    /// <inheritdoc />
    public partial class Delete_CounterOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderCounter",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: new Guid("39b5e8ef-0f83-450f-a037-83720976999f"),
                column: "OrderDate",
                value: new DateTime(2024, 3, 27, 3, 35, 39, 504, DateTimeKind.Utc).AddTicks(8768));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderCounter",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: new Guid("39b5e8ef-0f83-450f-a037-83720976999f"),
                columns: new[] { "OrderCounter", "OrderDate" },
                values: new object[] { 1, new DateTime(2024, 3, 27, 3, 25, 33, 688, DateTimeKind.Utc).AddTicks(2837) });
        }
    }
}
