using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace e_commerce.Data.Migrations
{
    /// <inheritdoc />
    public partial class FeatPasswordHashingSystemForAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Admins");

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "Admins",
                type: "BLOB",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "Admins",
                type: "BLOB",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Admins");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "Admins");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Admins",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
