using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComicManagerAPI.Migrations
{
    /// <inheritdoc />
    public partial class deleteIsReadVariable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRead",
                table: "Comics");

            migrationBuilder.AddColumn<bool>(
                name: "IsRead",
                table: "UsersComics",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2025, 9, 6, 20, 26, 56, 774, DateTimeKind.Local).AddTicks(1590));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRead",
                table: "UsersComics");

            migrationBuilder.AddColumn<bool>(
                name: "IsRead",
                table: "Comics",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2025, 6, 11, 18, 25, 18, 120, DateTimeKind.Local).AddTicks(3970));
        }
    }
}
