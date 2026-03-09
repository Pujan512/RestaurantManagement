using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class addPriceAttr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_AspNetUsers_WaiterId1",
                table: "Order");

            migrationBuilder.RenameColumn(
                name: "WaiterId1",
                table: "Order",
                newName: "UserId1");

            migrationBuilder.RenameColumn(
                name: "WaiterId",
                table: "Order",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_WaiterId1",
                table: "Order",
                newName: "IX_Order_UserId1");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "MenuItem",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_AspNetUsers_UserId1",
                table: "Order",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_AspNetUsers_UserId1",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "MenuItem");

            migrationBuilder.RenameColumn(
                name: "UserId1",
                table: "Order",
                newName: "WaiterId1");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Order",
                newName: "WaiterId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_UserId1",
                table: "Order",
                newName: "IX_Order_WaiterId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_AspNetUsers_WaiterId1",
                table: "Order",
                column: "WaiterId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
