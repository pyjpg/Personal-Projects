using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class DropAllTables1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7734d470-5d37-40e9-9d2d-c0cee8333469");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f3b7fdfe-b1a4-417f-8f1b-dc75d10977b2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a0a7fd20-d2d3-4bed-9ac9-8773600a6af6", null, "Member", "MEMBER" },
                    { "bf46e852-d8e4-4189-91be-be63ec23430f", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a0a7fd20-d2d3-4bed-9ac9-8773600a6af6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bf46e852-d8e4-4189-91be-be63ec23430f");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7734d470-5d37-40e9-9d2d-c0cee8333469", null, "Admin", "ADMIN" },
                    { "f3b7fdfe-b1a4-417f-8f1b-dc75d10977b2", null, "Member", "MEMBER" }
                });
        }
    }
}
