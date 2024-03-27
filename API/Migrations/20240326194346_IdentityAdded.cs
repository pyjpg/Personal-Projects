using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class IdentityAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2f3c5d14-ee85-4b52-b8c7-ea85fcb5ede3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "32d2ec77-cc97-42a6-befd-dd1fad76e9c4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7734d470-5d37-40e9-9d2d-c0cee8333469", null, "Admin", "ADMIN" },
                    { "f3b7fdfe-b1a4-417f-8f1b-dc75d10977b2", null, "Member", "MEMBER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "2f3c5d14-ee85-4b52-b8c7-ea85fcb5ede3", null, "Member", "MEMBER" },
                    { "32d2ec77-cc97-42a6-befd-dd1fad76e9c4", null, "Admin", "ADMIN" }
                });
        }
    }
}
