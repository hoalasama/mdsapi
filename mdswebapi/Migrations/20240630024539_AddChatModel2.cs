using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace mdswebapi.Migrations
{
    /// <inheritdoc />
    public partial class AddChatModel2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chat_AspNetUsers_ReceiverId",
                table: "Chat");

            migrationBuilder.DropForeignKey(
                name: "FK_Chat_AspNetUsers_SenderId",
                table: "Chat");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Chat",
                table: "Chat");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "09f186ea-982c-4744-a6cc-b29908675388");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "280f7455-d502-4f12-8e9c-8e10d571d259");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "781c822e-dc30-4350-a8eb-730cc9360f1e");

            migrationBuilder.RenameTable(
                name: "Chat",
                newName: "Chats");

            migrationBuilder.RenameIndex(
                name: "IX_Chat_SenderId",
                table: "Chats",
                newName: "IX_Chats_SenderId");

            migrationBuilder.RenameIndex(
                name: "IX_Chat_ReceiverId",
                table: "Chats",
                newName: "IX_Chats_ReceiverId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Chats",
                table: "Chats",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4bce6232-a59b-411d-970e-3e053ada513f", null, "Phar", "PHAR" },
                    { "67edc8dd-3cfa-49b5-93a3-d86651c8ec61", null, "User", "USER" },
                    { "c137121d-9371-4bdf-a871-98c63cf5c53b", null, "Admin", "ADMIN" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_AspNetUsers_ReceiverId",
                table: "Chats",
                column: "ReceiverId",
                principalTable: "AspNetUsers",
                principalColumn: "customerID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_AspNetUsers_SenderId",
                table: "Chats",
                column: "SenderId",
                principalTable: "AspNetUsers",
                principalColumn: "customerID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_AspNetUsers_ReceiverId",
                table: "Chats");

            migrationBuilder.DropForeignKey(
                name: "FK_Chats_AspNetUsers_SenderId",
                table: "Chats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Chats",
                table: "Chats");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4bce6232-a59b-411d-970e-3e053ada513f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "67edc8dd-3cfa-49b5-93a3-d86651c8ec61");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c137121d-9371-4bdf-a871-98c63cf5c53b");

            migrationBuilder.RenameTable(
                name: "Chats",
                newName: "Chat");

            migrationBuilder.RenameIndex(
                name: "IX_Chats_SenderId",
                table: "Chat",
                newName: "IX_Chat_SenderId");

            migrationBuilder.RenameIndex(
                name: "IX_Chats_ReceiverId",
                table: "Chat",
                newName: "IX_Chat_ReceiverId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Chat",
                table: "Chat",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "09f186ea-982c-4744-a6cc-b29908675388", null, "Admin", "ADMIN" },
                    { "280f7455-d502-4f12-8e9c-8e10d571d259", null, "Phar", "PHAR" },
                    { "781c822e-dc30-4350-a8eb-730cc9360f1e", null, "User", "USER" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Chat_AspNetUsers_ReceiverId",
                table: "Chat",
                column: "ReceiverId",
                principalTable: "AspNetUsers",
                principalColumn: "customerID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Chat_AspNetUsers_SenderId",
                table: "Chat",
                column: "SenderId",
                principalTable: "AspNetUsers",
                principalColumn: "customerID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
