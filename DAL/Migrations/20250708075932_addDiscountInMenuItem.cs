using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class addDiscountInMenuItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discount",
                table: "SystemSetting");

            migrationBuilder.AddColumn<decimal>(
                name: "Discount",
                table: "MenuItem",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discount",
                table: "MenuItem");

            migrationBuilder.AddColumn<decimal>(
                name: "Discount",
                table: "SystemSetting",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "SystemSetting",
                keyColumn: "Id",
                keyValue: 1234,
                column: "Discount",
                value: 0.0m);
        }
    }
}
