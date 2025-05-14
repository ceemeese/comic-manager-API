using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComicManagerAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DateCreated", "IsAdmin", "Mail", "Name", "Password", "Telephone" },
                values: new object[] { 1, new DateTime(2025, 5, 14, 12, 24, 42, 80, DateTimeKind.Local).AddTicks(6650), false, "admin@admin.com", "Admin", "admin", "666666666" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
