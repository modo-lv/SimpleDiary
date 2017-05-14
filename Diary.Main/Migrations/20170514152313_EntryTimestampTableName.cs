using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Diary.Main.Migrations
{
    public partial class EntryTimestampTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
/*
            migrationBuilder.DropForeignKey(
                name: "FK_EntryTimestamp_Entries_EntryId",
                table: "EntryTimestamp");
*/

/*
            migrationBuilder.DropPrimaryKey(
                name: "PK_EntryTimestamp",
                table: "EntryTimestamp");
*/

            migrationBuilder.RenameTable(
                name: "EntryTimestamp",
                newName: "EntryTimestamps");

/*
            migrationBuilder.RenameIndex(
                name: "IX_EntryTimestamp_EntryId",
                table: "EntryTimestamps",
                newName: "IX_EntryTimestamps_EntryId");
*/

/*
            migrationBuilder.AddPrimaryKey(
                name: "PK_EntryTimestamps",
                table: "EntryTimestamps",
                column: "Id");
*/

/*
            migrationBuilder.AddForeignKey(
                name: "FK_EntryTimestamps_Entries_EntryId",
                table: "EntryTimestamps",
                column: "EntryId",
                principalTable: "Entries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
*/
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
/*
            migrationBuilder.DropForeignKey(
                name: "FK_EntryTimestamps_Entries_EntryId",
                table: "EntryTimestamps");
*/

/*
            migrationBuilder.DropPrimaryKey(
                name: "PK_EntryTimestamps",
                table: "EntryTimestamps");
*/

            migrationBuilder.RenameTable(
                name: "EntryTimestamps",
                newName: "EntryTimestamp");

/*
            migrationBuilder.RenameIndex(
                name: "IX_EntryTimestamps_EntryId",
                table: "EntryTimestamp",
                newName: "IX_EntryTimestamp_EntryId");
*/

/*
            migrationBuilder.AddPrimaryKey(
                name: "PK_EntryTimestamp",
                table: "EntryTimestamp",
                column: "Id");
*/

/*
            migrationBuilder.AddForeignKey(
                name: "FK_EntryTimestamp_Entries_EntryId",
                table: "EntryTimestamp",
                column: "EntryId",
                principalTable: "Entries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
*/
        }
    }
}
