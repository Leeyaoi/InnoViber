using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InnoViber.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_Users_OwnerId",
                table: "Chats");

            migrationBuilder.DropForeignKey(
                name: "FK_Chats_Users_UserEntityId",
                table: "Chats");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Chats_ChatEntityId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ChatEntityId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Chats_OwnerId",
                table: "Chats");

            migrationBuilder.DropIndex(
                name: "IX_Chats_UserEntityId",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "ChatEntityId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "UserEntityId",
                table: "Chats");

            migrationBuilder.CreateTable(
                name: "ChatRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ChatId = table.Column<Guid>(type: "uuid", nullable: false),
                    ChatEntityId = table.Column<Guid>(type: "uuid", nullable: true),
                    UserEntityId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatRoles_Chats_ChatEntityId",
                        column: x => x.ChatEntityId,
                        principalTable: "Chats",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChatRoles_Users_UserEntityId",
                        column: x => x.UserEntityId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatRoles_ChatEntityId",
                table: "ChatRoles",
                column: "ChatEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatRoles_UserEntityId",
                table: "ChatRoles",
                column: "UserEntityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatRoles");

            migrationBuilder.AddColumn<Guid>(
                name: "ChatEntityId",
                table: "Users",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "Chats",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserEntityId",
                table: "Chats",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ChatEntityId",
                table: "Users",
                column: "ChatEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_OwnerId",
                table: "Chats",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_UserEntityId",
                table: "Chats",
                column: "UserEntityId");

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
                name: "FK_Users_Chats_ChatEntityId",
                table: "Users",
                column: "ChatEntityId",
                principalTable: "Chats",
                principalColumn: "Id");
        }
    }
}
