using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Z05.Migrations
{
    public partial class AttributeNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NoteCategory_Categories_CategoryId",
                table: "NoteCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_NoteCategory_Notes_NoteId",
                table: "NoteCategory");

            migrationBuilder.DropIndex(
                name: "IX_Categories_Name",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "NoteId",
                table: "Notes",
                newName: "NoteID");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "NoteCategory",
                newName: "CategoryID");

            migrationBuilder.RenameColumn(
                name: "NoteId",
                table: "NoteCategory",
                newName: "NoteID");

            migrationBuilder.RenameIndex(
                name: "IX_NoteCategory_CategoryId",
                table: "NoteCategory",
                newName: "IX_NoteCategory_CategoryID");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Categories",
                newName: "CategoryID");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Notes",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Notes",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "NoteDate",
                table: "Notes",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Categories",
                maxLength: 64,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Title",
                table: "Categories",
                column: "Title",
                unique: true,
                filter: "[Title] IS NOT NULL");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NoteCategory_Categories_CategoryID",
                table: "NoteCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_NoteCategory_Notes_NoteID",
                table: "NoteCategory");

            migrationBuilder.DropIndex(
                name: "IX_Categories_Title",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "NoteDate",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "NoteID",
                table: "Notes",
                newName: "NoteId");

            migrationBuilder.RenameColumn(
                name: "CategoryID",
                table: "NoteCategory",
                newName: "CategoryId");

            migrationBuilder.RenameColumn(
                name: "NoteID",
                table: "NoteCategory",
                newName: "NoteId");

            migrationBuilder.RenameIndex(
                name: "IX_NoteCategory_CategoryID",
                table: "NoteCategory",
                newName: "IX_NoteCategory_CategoryId");

            migrationBuilder.RenameColumn(
                name: "CategoryID",
                table: "Categories",
                newName: "CategoryId");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Notes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 64);

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Notes",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Notes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_NoteCategory_Categories_CategoryId",
                table: "NoteCategory",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NoteCategory_Notes_NoteId",
                table: "NoteCategory",
                column: "NoteId",
                principalTable: "Notes",
                principalColumn: "NoteId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
