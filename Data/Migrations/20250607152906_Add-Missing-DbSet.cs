using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class AddMissingDbSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAddressEntity_UserProfiles_UserId",
                table: "UserAddressEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAddressEntity",
                table: "UserAddressEntity");

            migrationBuilder.RenameTable(
                name: "UserAddressEntity",
                newName: "UserAddress");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAddress",
                table: "UserAddress",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAddress_UserProfiles_UserId",
                table: "UserAddress",
                column: "UserId",
                principalTable: "UserProfiles",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAddress_UserProfiles_UserId",
                table: "UserAddress");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAddress",
                table: "UserAddress");

            migrationBuilder.RenameTable(
                name: "UserAddress",
                newName: "UserAddressEntity");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAddressEntity",
                table: "UserAddressEntity",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAddressEntity_UserProfiles_UserId",
                table: "UserAddressEntity",
                column: "UserId",
                principalTable: "UserProfiles",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
