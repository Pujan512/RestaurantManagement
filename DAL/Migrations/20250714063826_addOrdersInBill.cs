using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class addOrdersInBill : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BillId",
                table: "Order",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Order_BillId",
                table: "Order",
                column: "BillId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Bill_BillId",
                table: "Order",
                column: "BillId",
                principalTable: "Bill",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Bill_BillId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_BillId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "BillId",
                table: "Order");
        }
    }
}
