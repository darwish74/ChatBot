using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatBot.Migrations
{
    /// <inheritdoc />
    public partial class tt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Messages_ParentMessageMessageId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_ParentMessageMessageId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "ParentMessageMessageId",
                table: "Messages");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "Messages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParentMessageMessageId",
                table: "Messages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ParentMessageMessageId",
                table: "Messages",
                column: "ParentMessageMessageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Messages_ParentMessageMessageId",
                table: "Messages",
                column: "ParentMessageMessageId",
                principalTable: "Messages",
                principalColumn: "MessageId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
