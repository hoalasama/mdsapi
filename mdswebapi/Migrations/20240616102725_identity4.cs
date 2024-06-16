using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace mdswebapi.Migrations
{
    /// <inheritdoc />
    public partial class identity4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "419a99e1-cb6f-4eb8-bd68-a4a3905ae68a", null, "Admin", "ADMIN" },
                    { "4522e256-da58-4901-b09c-592973085579", null, "Phar", "PHAR" },
                    { "faee2582-1188-4fd7-8ee8-5aab34086261", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "419a99e1-cb6f-4eb8-bd68-a4a3905ae68a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4522e256-da58-4901-b09c-592973085579");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "faee2582-1188-4fd7-8ee8-5aab34086261");
        }
    }
}
