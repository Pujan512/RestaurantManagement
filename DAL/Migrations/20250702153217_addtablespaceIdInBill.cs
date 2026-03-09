using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class addtablespaceIdInBill : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "TablespaceId",
                table: "Bill",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Bill_TablespaceId",
                table: "Bill",
                column: "TablespaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bill_Tablespace_TablespaceId",
                table: "Bill",
                column: "TablespaceId",
                principalTable: "Tablespace",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bill_Tablespace_TablespaceId",
                table: "Bill");

            migrationBuilder.DropIndex(
                name: "IX_Bill_TablespaceId",
                table: "Bill");

            migrationBuilder.DropColumn(
                name: "TablespaceId",
                table: "Bill");

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
    }
}
