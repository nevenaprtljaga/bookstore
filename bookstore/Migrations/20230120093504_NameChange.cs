using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bookstore.Migrations
{
    /// <inheritdoc />
    public partial class NameChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_User_UserId",
                table: "Book");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_User_UserId",
                table: "Order");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Order",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_UserId",
                table: "Order",
                newName: "IX_Order_ApplicationUserId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Book",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Book_UserId",
                table: "Book",
                newName: "IX_Book_ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_User_ApplicationUserId",
                table: "Book",
                column: "ApplicationUserId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_User_ApplicationUserId",
                table: "Order",
                column: "ApplicationUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_User_ApplicationUserId",
                table: "Book");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_User_ApplicationUserId",
                table: "Order");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Order",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_ApplicationUserId",
                table: "Order",
                newName: "IX_Order_UserId");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Book",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Book_ApplicationUserId",
                table: "Book",
                newName: "IX_Book_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_User_UserId",
                table: "Book",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_User_UserId",
                table: "Order",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
