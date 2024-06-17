using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace mdswebapi.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "107fd690-95d2-4122-b998-84b19a162cda");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1abffd07-1a14-42fd-a212-c4762ced4889");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4b3350bb-5abf-4154-9af0-878008072772");

            migrationBuilder.AddColumn<string>(
                name: "CustomerId",
                table: "pharmacies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "417effc9-307a-41e5-837e-8bd4afd44678", null, "Admin", "ADMIN" },
                    { "5b3b4291-6de4-4b72-b112-d5cb09c5c055", null, "User", "USER" },
                    { "d5195425-7e7c-4de3-b69d-a2d369e1dbb0", null, "Phar", "PHAR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "417effc9-307a-41e5-837e-8bd4afd44678");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5b3b4291-6de4-4b72-b112-d5cb09c5c055");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d5195425-7e7c-4de3-b69d-a2d369e1dbb0");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "pharmacies");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "107fd690-95d2-4122-b998-84b19a162cda", null, "Admin", "ADMIN" },
                    { "1abffd07-1a14-42fd-a212-c4762ced4889", null, "Phar", "PHAR" },
                    { "4b3350bb-5abf-4154-9af0-878008072772", null, "User", "USER" }
                });
        }
    }
}
