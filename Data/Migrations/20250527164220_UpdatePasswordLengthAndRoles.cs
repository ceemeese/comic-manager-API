using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComicManagerAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePasswordLengthAndRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComicGenre_Comics_ComicId",
                table: "ComicGenre");

            migrationBuilder.DropForeignKey(
                name: "FK_ComicGenre_Genres_GenreId",
                table: "ComicGenre");

            migrationBuilder.DropForeignKey(
                name: "FK_UserComic_Comics_ComicId",
                table: "UserComic");

            migrationBuilder.DropForeignKey(
                name: "FK_UserComic_Users_UserId",
                table: "UserComic");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserComic",
                table: "UserComic");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ComicGenre",
                table: "ComicGenre");

            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "UserComic",
                newName: "UsersComics");

            migrationBuilder.RenameTable(
                name: "ComicGenre",
                newName: "ComicsGenres");

            migrationBuilder.RenameIndex(
                name: "IX_UserComic_ComicId",
                table: "UsersComics",
                newName: "IX_UsersComics_ComicId");

            migrationBuilder.RenameIndex(
                name: "IX_ComicGenre_GenreId",
                table: "ComicsGenres",
                newName: "IX_ComicsGenres_GenreId");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "varchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Users",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<decimal>(
                name: "PercentageOfComics",
                table: "Genres",
                type: "decimal(5,2)",
                precision: 5,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Comics",
                type: "decimal(5,2)",
                precision: 5,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersComics",
                table: "UsersComics",
                columns: new[] { "UserId", "ComicId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ComicsGenres",
                table: "ComicsGenres",
                columns: new[] { "ComicId", "GenreId" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "Role" },
                values: new object[] { new DateTime(2025, 5, 27, 18, 42, 20, 85, DateTimeKind.Local).AddTicks(3710), "admin" });

            migrationBuilder.AddForeignKey(
                name: "FK_ComicsGenres_Comics_ComicId",
                table: "ComicsGenres",
                column: "ComicId",
                principalTable: "Comics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ComicsGenres_Genres_GenreId",
                table: "ComicsGenres",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersComics_Comics_ComicId",
                table: "UsersComics",
                column: "ComicId",
                principalTable: "Comics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersComics_Users_UserId",
                table: "UsersComics",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComicsGenres_Comics_ComicId",
                table: "ComicsGenres");

            migrationBuilder.DropForeignKey(
                name: "FK_ComicsGenres_Genres_GenreId",
                table: "ComicsGenres");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersComics_Comics_ComicId",
                table: "UsersComics");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersComics_Users_UserId",
                table: "UsersComics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersComics",
                table: "UsersComics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ComicsGenres",
                table: "ComicsGenres");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "UsersComics",
                newName: "UserComic");

            migrationBuilder.RenameTable(
                name: "ComicsGenres",
                newName: "ComicGenre");

            migrationBuilder.RenameIndex(
                name: "IX_UsersComics_ComicId",
                table: "UserComic",
                newName: "IX_UserComic_ComicId");

            migrationBuilder.RenameIndex(
                name: "IX_ComicsGenres_GenreId",
                table: "ComicGenre",
                newName: "IX_ComicGenre_GenreId");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldMaxLength: 10)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "Users",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<decimal>(
                name: "PercentageOfComics",
                table: "Genres",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)",
                oldPrecision: 5,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Comics",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)",
                oldPrecision: 5,
                oldScale: 2);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserComic",
                table: "UserComic",
                columns: new[] { "UserId", "ComicId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ComicGenre",
                table: "ComicGenre",
                columns: new[] { "ComicId", "GenreId" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "IsAdmin" },
                values: new object[] { new DateTime(2025, 5, 14, 12, 24, 42, 80, DateTimeKind.Local).AddTicks(6650), false });

            migrationBuilder.AddForeignKey(
                name: "FK_ComicGenre_Comics_ComicId",
                table: "ComicGenre",
                column: "ComicId",
                principalTable: "Comics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ComicGenre_Genres_GenreId",
                table: "ComicGenre",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserComic_Comics_ComicId",
                table: "UserComic",
                column: "ComicId",
                principalTable: "Comics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserComic_Users_UserId",
                table: "UserComic",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
