using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Z05.Migrations
{
    public partial class TableNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NoteCategory_Categories_CategoryID",
                table: "NoteCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_NoteCategory_Notes_NoteID",
                table: "NoteCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notes",
                table: "Notes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.EnsureSchema(
                name: "wnuk");

            migrationBuilder.RenameTable(
                name: "NoteCategory",
                newName: "NoteCategory",
                newSchema: "wnuk");

            migrationBuilder.RenameTable(
                name: "Notes",
                newName: "Note",
                newSchema: "wnuk");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Category",
                newSchema: "wnuk");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_Title",
                schema: "wnuk",
                table: "Category",
                newName: "IX_Category_Title");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Note",
                schema: "wnuk",
                table: "Note",
                column: "NoteID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                schema: "wnuk",
                table: "Category",
                column: "CategoryID");

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

            migrationBuilder.AddForeignKey(
                name: "FK_NoteCategory_Category_CategoryID",
                schema: "wnuk",
                table: "NoteCategory",
                column: "CategoryID",
                principalSchema: "wnuk",
                principalTable: "Category",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NoteCategory_Note_NoteID",
                schema: "wnuk",
                table: "NoteCategory",
                column: "NoteID",
                principalSchema: "wnuk",
                principalTable: "Note",
                principalColumn: "NoteID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NoteCategory_Category_CategoryID",
                schema: "wnuk",
                table: "NoteCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_NoteCategory_Note_NoteID",
                schema: "wnuk",
                table: "NoteCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Note",
                schema: "wnuk",
                table: "Note");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                schema: "wnuk",
                table: "Category");

            migrationBuilder.RenameTable(
                name: "NoteCategory",
                schema: "wnuk",
                newName: "NoteCategory");

            migrationBuilder.RenameTable(
                name: "Note",
                schema: "wnuk",
                newName: "Notes");

            migrationBuilder.RenameTable(
                name: "Category",
                schema: "wnuk",
                newName: "Categories");

            migrationBuilder.RenameIndex(
                name: "IX_Category_Title",
                table: "Categories",
                newName: "IX_Categories_Title");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notes",
                table: "Notes",
                column: "NoteID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "CategoryID");

            migrationBuilder.UpdateData(
                table: "Notes",
                keyColumn: "NoteID",
                keyValue: 1,
                column: "NoteDate",
                value: new DateTime(2019, 11, 16, 20, 12, 25, 755, DateTimeKind.Local).AddTicks(1686));

            migrationBuilder.UpdateData(
                table: "Notes",
                keyColumn: "NoteID",
                keyValue: 2,
                column: "NoteDate",
                value: new DateTime(2019, 11, 16, 20, 12, 25, 759, DateTimeKind.Local).AddTicks(3176));

            migrationBuilder.UpdateData(
                table: "Notes",
                keyColumn: "NoteID",
                keyValue: 3,
                column: "NoteDate",
                value: new DateTime(2019, 11, 16, 20, 12, 25, 759, DateTimeKind.Local).AddTicks(3220));

            migrationBuilder.AddForeignKey(
                name: "FK_NoteCategory_Categories_CategoryID",
                table: "NoteCategory",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NoteCategory_Notes_NoteID",
                table: "NoteCategory",
                column: "NoteID",
                principalTable: "Notes",
                principalColumn: "NoteID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
