using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InnoViber.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ChangeUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatRoles_Chats_ChatEntityId",
                table: "ChatRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatRoles_Users_UserEntityId",
                table: "ChatRoles");

            migrationBuilder.DropIndex(
                name: "IX_ChatRoles_ChatEntityId",
                table: "ChatRoles");

            migrationBuilder.DropIndex(
                name: "IX_ChatRoles_UserEntityId",
                table: "ChatRoles");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ChatEntityId",
                table: "ChatRoles");

            migrationBuilder.DropColumn(
                name: "UserEntityId",
                table: "ChatRoles");

            migrationBuilder.AddColumn<Guid>(
                name: "MongoId",
                table: "Users",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ChatRoles_ChatId",
                table: "ChatRoles",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatRoles_UserId",
                table: "ChatRoles",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatRoles_Chats_ChatId",
                table: "ChatRoles",
                column: "ChatId",
                principalTable: "Chats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatRoles_Users_UserId",
                table: "ChatRoles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatRoles_Chats_ChatId",
                table: "ChatRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatRoles_Users_UserId",
                table: "ChatRoles");

            migrationBuilder.DropIndex(
                name: "IX_ChatRoles_ChatId",
                table: "ChatRoles");

            migrationBuilder.DropIndex(
                name: "IX_ChatRoles_UserId",
                table: "ChatRoles");

            migrationBuilder.DropColumn(
                name: "MongoId",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "ChatEntityId",
                table: "ChatRoles",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserEntityId",
                table: "ChatRoles",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChatRoles_ChatEntityId",
                table: "ChatRoles",
                column: "ChatEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatRoles_UserEntityId",
                table: "ChatRoles",
                column: "UserEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatRoles_Chats_ChatEntityId",
                table: "ChatRoles",
                column: "ChatEntityId",
                principalTable: "Chats",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatRoles_Users_UserEntityId",
                table: "ChatRoles",
                column: "UserEntityId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
