using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Z05.Migrations
{
    public partial class DefaultValues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryID", "Title" },
                values: new object[,]
                {
                    { 1, "zadanie" },
                    { 2, "sprawko" },
                    { 3, "informatyka" }
                });

            migrationBuilder.InsertData(
                table: "Notes",
                columns: new[] { "NoteID", "Description", "IsMarkdownFile", "NoteDate", "Title" },
                values: new object[,]
                {
                    { 1, "Pierwsza notatka", false, new DateTime(2019, 11, 16, 20, 12, 25, 755, DateTimeKind.Local).AddTicks(1686), "Zakupy" },
                    { 2, "Druga notatka", false, new DateTime(2019, 11, 16, 20, 12, 25, 759, DateTimeKind.Local).AddTicks(3176), "Zabieg" },
                    { 3, "Trzecia notatka", false, new DateTime(2019, 11, 16, 20, 12, 25, 759, DateTimeKind.Local).AddTicks(3220), "Wyjazd" }
                });

            migrationBuilder.InsertData(
                table: "NoteCategory",
                columns: new[] { "NoteID", "CategoryID" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 2 },
                    { 3, 3 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "NoteCategory",
                keyColumns: new[] { "NoteID", "CategoryID" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "NoteCategory",
                keyColumns: new[] { "NoteID", "CategoryID" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "NoteCategory",
                keyColumns: new[] { "NoteID", "CategoryID" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "NoteCategory",
                keyColumns: new[] { "NoteID", "CategoryID" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Notes",
                keyColumn: "NoteID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Notes",
                keyColumn: "NoteID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Notes",
                keyColumn: "NoteID",
                keyValue: 3);
        }
    }
}
