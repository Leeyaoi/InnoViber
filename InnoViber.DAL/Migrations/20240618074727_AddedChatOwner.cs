using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InnoViber.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddedChatOwner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_Users_UserId",
                table: "Chats");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Chats_ChatId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_UserId",
                table: "Messages");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Chats",
                newName: "UserEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_Chats_UserId",
                table: "Chats",
                newName: "IX_Chats_UserEntityId");

            migrationBuilder.AddColumn<Guid>(
                name: "ChatEntityId",
                table: "Users",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Messages",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ChatId",
                table: "Messages",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "Chats",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Users_ChatEntityId",
                table: "Users",
                column: "ChatEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_OwnerId",
                table: "Chats",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_Users_OwnerId",
                table: "Chats",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_Users_UserEntityId",
                table: "Chats",
                column: "UserEntityId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Chats_ChatId",
                table: "Messages",
                column: "ChatId",
                principalTable: "Chats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_UserId",
                table: "Messages",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Chats_ChatEntityId",
                table: "Users",
                column: "ChatEntityId",
                principalTable: "Chats",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_Users_OwnerId",
                table: "Chats");

            migrationBuilder.DropForeignKey(
                name: "FK_Chats_Users_UserEntityId",
                table: "Chats");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Chats_ChatId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_UserId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Chats_ChatEntityId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ChatEntityId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Chats_OwnerId",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "ChatEntityId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Chats");

            migrationBuilder.RenameColumn(
                name: "UserEntityId",
                table: "Chats",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Chats_UserEntityId",
                table: "Chats",
                newName: "IX_Chats_UserId");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Messages",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "ChatId",
                table: "Messages",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_Users_UserId",
                table: "Chats",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Chats_ChatId",
                table: "Messages",
                column: "ChatId",
                principalTable: "Chats",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_UserId",
                table: "Messages",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
