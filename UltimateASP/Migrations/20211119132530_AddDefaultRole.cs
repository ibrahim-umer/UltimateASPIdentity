using Microsoft.EntityFrameworkCore.Migrations;

namespace UltimateASP.Migrations
{
    public partial class AddDefaultRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "28382093-408e-4a3d-b4fe-ecb2bc70422b", "8cc0b6a4-e2c0-4180-be42-75aac26fc0ba", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6428bbea-a9ed-453d-bb79-4e18a7a7648b", "320d91c0-e264-4176-9186-7d43ac0744ab", "User", "User" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3919dc95-b92a-4ae2-8805-3c85aaffebd7", "6436bfbf-e8ed-4449-a4cd-0b3f28a74fe6", "Manager", "Manager" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "28382093-408e-4a3d-b4fe-ecb2bc70422b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3919dc95-b92a-4ae2-8805-3c85aaffebd7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6428bbea-a9ed-453d-bb79-4e18a7a7648b");
        }
    }
}
