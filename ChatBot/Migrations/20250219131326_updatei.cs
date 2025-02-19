using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatBot.Migrations
{
    /// <inheritdoc />
    public partial class updatei : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Channels_AspNetUsers_OwnerId1",
                table: "Channels");

            migrationBuilder.DropIndex(
                name: "IX_Channels_OwnerId1",
                table: "Channels");

            migrationBuilder.DropColumn(
                name: "OwnerId1",
                table: "Channels");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "Channels",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Channels_OwnerId",
                table: "Channels",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Channels_AspNetUsers_OwnerId",
                table: "Channels",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Channels_AspNetUsers_OwnerId",
                table: "Channels");

            migrationBuilder.DropIndex(
                name: "IX_Channels_OwnerId",
                table: "Channels");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Messages",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OwnerId",
                table: "Channels",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "OwnerId1",
                table: "Channels",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Channels_OwnerId1",
                table: "Channels",
                column: "OwnerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Channels_AspNetUsers_OwnerId1",
                table: "Channels",
                column: "OwnerId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
