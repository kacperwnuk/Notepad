using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Z05.Migrations
{
    public partial class NewDefaultValues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "wnuk",
                table: "Category",
                columns: new[] { "CategoryID", "Title" },
                values: new object[,]
                {
                    { 4, "zakupy" },
                    { 5, "impreza" },
                    { 6, "warzywa" }
                });

            migrationBuilder.UpdateData(
                schema: "wnuk",
                table: "Note",
                keyColumn: "NoteID",
                keyValue: 1,
                column: "NoteDate",
                value: new DateTime(2019, 11, 21, 14, 9, 37, 431, DateTimeKind.Local).AddTicks(7864));

            migrationBuilder.UpdateData(
                schema: "wnuk",
                table: "Note",
                keyColumn: "NoteID",
                keyValue: 2,
                column: "NoteDate",
                value: new DateTime(2019, 11, 21, 14, 9, 37, 436, DateTimeKind.Local).AddTicks(280));

            migrationBuilder.UpdateData(
                schema: "wnuk",
                table: "Note",
                keyColumn: "NoteID",
                keyValue: 3,
                column: "NoteDate",
                value: new DateTime(2019, 11, 21, 14, 9, 37, 436, DateTimeKind.Local).AddTicks(331));

            migrationBuilder.InsertData(
                schema: "wnuk",
                table: "Note",
                columns: new[] { "NoteID", "Description", "IsMarkdownFile", "NoteDate", "Title" },
                values: new object[] { 4, "Czwarta notatka", false, new DateTime(2019, 11, 21, 14, 9, 37, 436, DateTimeKind.Local).AddTicks(337), "Misja" });

            migrationBuilder.InsertData(
                schema: "wnuk",
                table: "Note",
                columns: new[] { "NoteID", "Description", "IsMarkdownFile", "NoteDate", "Title" },
                values: new object[] { 5, "Kolejna notatka", false, new DateTime(2019, 11, 21, 14, 9, 37, 436, DateTimeKind.Local).AddTicks(340), "Szkolenie" });

            migrationBuilder.InsertData(
                schema: "wnuk",
                table: "Note",
                columns: new[] { "NoteID", "Description", "IsMarkdownFile", "NoteDate", "Title" },
                values: new object[] { 6, "Zadanie domowe", false, new DateTime(2019, 11, 21, 14, 9, 37, 436, DateTimeKind.Local).AddTicks(343), "Wycieczka" });

            migrationBuilder.InsertData(
                schema: "wnuk",
                table: "NoteCategory",
                columns: new[] { "NoteID", "CategoryID" },
                values: new object[] { 4, 4 });

            migrationBuilder.InsertData(
                schema: "wnuk",
                table: "NoteCategory",
                columns: new[] { "NoteID", "CategoryID" },
                values: new object[] { 5, 5 });

            migrationBuilder.InsertData(
                schema: "wnuk",
                table: "NoteCategory",
                columns: new[] { "NoteID", "CategoryID" },
                values: new object[] { 6, 6 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "wnuk",
                table: "NoteCategory",
                keyColumns: new[] { "NoteID", "CategoryID" },
                keyValues: new object[] { 4, 4 });

            migrationBuilder.DeleteData(
                schema: "wnuk",
                table: "NoteCategory",
                keyColumns: new[] { "NoteID", "CategoryID" },
                keyValues: new object[] { 5, 5 });

            migrationBuilder.DeleteData(
                schema: "wnuk",
                table: "NoteCategory",
                keyColumns: new[] { "NoteID", "CategoryID" },
                keyValues: new object[] { 6, 6 });

            migrationBuilder.DeleteData(
                schema: "wnuk",
                table: "Category",
                keyColumn: "CategoryID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "wnuk",
                table: "Category",
                keyColumn: "CategoryID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "wnuk",
                table: "Category",
                keyColumn: "CategoryID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                schema: "wnuk",
                table: "Note",
                keyColumn: "NoteID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "wnuk",
                table: "Note",
                keyColumn: "NoteID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "wnuk",
                table: "Note",
                keyColumn: "NoteID",
                keyValue: 6);

            migrationBuilder.UpdateData(
                schema: "wnuk",
                table: "Note",
                keyColumn: "NoteID",
                keyValue: 1,
                column: "NoteDate",
                value: new DateTime(2019, 11, 20, 19, 25, 20, 526, DateTimeKind.Local).AddTicks(7055));

            migrationBuilder.UpdateData(
                schema: "wnuk",
                table: "Note",
                keyColumn: "NoteID",
                keyValue: 2,
                column: "NoteDate",
                value: new DateTime(2019, 11, 20, 19, 25, 20, 531, DateTimeKind.Local).AddTicks(405));

            migrationBuilder.UpdateData(
                schema: "wnuk",
                table: "Note",
                keyColumn: "NoteID",
                keyValue: 3,
                column: "NoteDate",
                value: new DateTime(2019, 11, 20, 19, 25, 20, 531, DateTimeKind.Local).AddTicks(457));
        }
    }
}
