using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InnoViber.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Deleted_user : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatRoles_Chats_ChatId",
                table: "ChatRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatRoles_Users_UserId",
                table: "ChatRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_UserId",
                table: "Messages");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Messages_UserId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_ChatRoles_ChatId",
                table: "ChatRoles");

            migrationBuilder.DropIndex(
                name: "IX_ChatRoles_UserId",
                table: "ChatRoles");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Messages",
                type: "text",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ChatRoles",
                type: "text",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.CreateIndex(
                name: "IX_ChatRoles_ChatId",
                table: "ChatRoles",
                column: "ChatId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatRoles_Chats_ChatId",
                table: "ChatRoles",
                column: "ChatId",
                principalTable: "Chats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatRoles_Chats_ChatId",
                table: "ChatRoles");

            migrationBuilder.DropIndex(
                name: "IX_ChatRoles_ChatId",
                table: "ChatRoles");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Messages",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "ChatRoles",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Surname = table.Column<string>(type: "text", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_UserId",
                table: "Messages",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatRoles_ChatEntityId",
                table: "ChatRoles",
                column: "ChatEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatRoles_UserEntityId",
                table: "ChatRoles",
                column: "UserEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatRoles_Chats_ChatId",
                table: "ChatRoles",
                column: "ChatEntityId",
                principalTable: "Chats",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatRoles_Users_UserId",
                table: "ChatRoles",
                column: "UserEntityId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_UserId",
                table: "Messages",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
