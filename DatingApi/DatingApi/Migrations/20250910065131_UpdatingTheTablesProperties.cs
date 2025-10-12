using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatingApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdatingTheTablesProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastDate",
                table: "Members",
                newName: "LastActive");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "LastActive",
                table: "Members",
                newName: "LastDate");
        }
    }
}
