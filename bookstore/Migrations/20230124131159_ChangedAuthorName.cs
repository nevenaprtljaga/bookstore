using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bookstore.Migrations
{
    /// <inheritdoc />
    public partial class ChangedAuthorName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Author");

            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "Author",
                newName: "FullName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Author",
                newName: "Surname");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Author",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
