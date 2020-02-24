using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Z05.Migrations
{
    public partial class RowVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                schema: "wnuk",
                table: "Note",
                rowVersion: true,
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowVersion",
                schema: "wnuk",
                table: "Note");

            migrationBuilder.UpdateData(
                schema: "wnuk",
                table: "Note",
                keyColumn: "NoteID",
                keyValue: 1,
                column: "NoteDate",
                value: new DateTime(2019, 11, 20, 19, 16, 19, 179, DateTimeKind.Local).AddTicks(6534));

            migrationBuilder.UpdateData(
                schema: "wnuk",
                table: "Note",
                keyColumn: "NoteID",
                keyValue: 2,
                column: "NoteDate",
                value: new DateTime(2019, 11, 20, 19, 16, 19, 183, DateTimeKind.Local).AddTicks(7467));

            migrationBuilder.UpdateData(
                schema: "wnuk",
                table: "Note",
                keyColumn: "NoteID",
                keyValue: 3,
                column: "NoteDate",
                value: new DateTime(2019, 11, 20, 19, 16, 19, 183, DateTimeKind.Local).AddTicks(7511));
        }
    }
}
