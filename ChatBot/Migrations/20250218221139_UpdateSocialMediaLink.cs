using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatBot.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSocialMediaLink : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSocialMedias_AspNetUsers_UserId1",
                table: "UserSocialMedias");

            migrationBuilder.DropIndex(
                name: "IX_UserSocialMedias_UserId1",
                table: "UserSocialMedias");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "UserSocialMedias");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserSocialMedias",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_UserSocialMedias_UserId",
                table: "UserSocialMedias",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSocialMedias_AspNetUsers_UserId",
                table: "UserSocialMedias",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSocialMedias_AspNetUsers_UserId",
                table: "UserSocialMedias");

            migrationBuilder.DropIndex(
                name: "IX_UserSocialMedias_UserId",
                table: "UserSocialMedias");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "UserSocialMedias",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "UserSocialMedias",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_UserSocialMedias_UserId1",
                table: "UserSocialMedias",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSocialMedias_AspNetUsers_UserId1",
                table: "UserSocialMedias",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
