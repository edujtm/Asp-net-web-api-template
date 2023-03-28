using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Asp_net_web_api_template.Migrations
{
    /// <inheritdoc />
    public partial class AddRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "46711c99-80bf-4d42-8b99-18af1261a100", "28290bd6-e2e0-4b82-894b-ce52b2ec49ef", "Manager", "MANAGER" },
                    { "c0c43bfd-fae2-4201-8fb6-56d262624aae", "2b3b2ec8-3c69-44a4-a729-711b5066da29", "Administrator", "ADMINISTRATOR" },
                    { "d11142c9-4db0-46e4-a8cd-577f91459d60", "fafeb923-9437-4734-9c06-fb73ce88e813", "Customer", "CUSTOMER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "46711c99-80bf-4d42-8b99-18af1261a100");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c0c43bfd-fae2-4201-8fb6-56d262624aae");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d11142c9-4db0-46e4-a8cd-577f91459d60");
        }
    }
}
