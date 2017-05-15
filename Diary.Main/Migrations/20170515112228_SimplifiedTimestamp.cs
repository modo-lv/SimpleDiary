using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Diary.Main.Migrations
{
	public partial class SimplifiedTimestamp : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			// Add the new column
			migrationBuilder.AddColumn<long>(
				name: "Timestamp",
				table: "Entries",
				nullable: false,
				defaultValue: 0L);

			// Move data from timestamp table to column
			migrationBuilder.Sql("PRAGMA foreign_keys = OFF;");
			migrationBuilder.Sql(
				"REPLACE INTO Entries (Id, Title, Content, Timestamp) " +
				"SELECT E.Id, E.Title, E.Content, Et.Timestamp FROM Entries AS E " +
				"INNER JOIN EntryTimestamps AS Et ON E.Id = Et.EntryId ");

			// Drop the old table
			migrationBuilder.DropTable(
				name: "EntryTimestamps");

		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			// Create the timestamp table
			migrationBuilder.CreateTable(
				name: "EntryTimestamps",
				columns: table => new
				{
					Id = table.Column<uint>(nullable: false)
						.Annotation("Sqlite:Autoincrement", true),
					EntryId = table.Column<uint>(nullable: true),
					Timestamp = table.Column<long>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_EntryTimestamps", x => x.Id);
					table.ForeignKey(
						name: "FK_EntryTimestamps_Entries_EntryId",
						column: x => x.EntryId,
						principalTable: "Entries",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateIndex(
				name: "IX_EntryTimestamps_EntryId",
				table: "EntryTimestamps",
				column: "EntryId");


			// Move data from Timestamp column to Timestamp table
			migrationBuilder.Sql(
				"REPLACE INTO EntryTimestamps (EntryId, Timestamp) " +
				"SELECT E.Id, E.Timestamp FROM Entries AS E " +
				"WHERE E.Timestamp IS NOT NULL");

			// Drop the Timestamp column.
			migrationBuilder.DropColumn(
				name: "Timestamp",
				table: "Entries");


		}
	}
}