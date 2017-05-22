using System;
using System.Collections.Generic;
using Diary.Main.Domain.Entities;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Diary.Main.Migrations
{
	public partial class SplitEntryTypes : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.Sql("DROP TABLE IF EXISTS __entries__data__");
			migrationBuilder.Sql("CREATE TABLE __entries__data__ AS SELECT * FROM Entries");

			migrationBuilder.DropTable("Entries");

			migrationBuilder.CreateTable(
				"Entries",
				table => new {
					Id = table.Column<UInt32>(nullable: false)
						.Annotation("Sqlite:Autoincrement", value: true),
					Timestamp = table.Column<Int64>(nullable: false),
					Type = table.Column<Int32>(nullable: false, defaultValue:(Int32)FileEntryType.Generic),
					Name = table.Column<String>(nullable: true),
					Description = table.Column<String>(nullable: true),
					IsDeleted = table.Column<Boolean>(nullable: false, defaultValue: false),

					FileContentId = table.Column<UInt32>(nullable:true),
					TextContentId = table.Column<UInt32>(nullable:true)
				},
				constraints: table => {
					table.PrimaryKey("PK_Entries", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "EntryFileContent",
				columns: table => new
				{
					Id = table.Column<UInt32>(nullable: false)
						.Annotation("Sqlite:Autoincrement", true),
					FileName = table.Column<String>(nullable: true),
					FileType = table.Column<Int32>(nullable: false, defaultValue:(Int32)FileEntryType.Image)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_EntryFileContent", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "EntryTextContent",
				columns: table => new
				{
					Id = table.Column<UInt32>(nullable: false)
						.Annotation("Sqlite:Autoincrement", true),
					Content = table.Column<String>(nullable: true),
					Format = table.Column<Int32>(nullable: false, defaultValue:(Int32)TextContentFormat.Plain)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_EntryTextContent", x => x.Id);
				});

			migrationBuilder.CreateIndex(
				name: "IX_Entries_FileContentId",
				table: "Entries",
				column: "FileContentId");

			migrationBuilder.CreateIndex(
				name: "IX_Entries_TextContentId",
				table: "Entries",
				column: "TextContentId");

			migrationBuilder.AddForeignKey(
				name: "FK_Entries_EntryFileContent_FileContentId",
				table: "Entries",
				column: "FileContentId",
				principalTable: "EntryFileContent",
				principalColumn: "Id",
				onDelete: ReferentialAction.Restrict);

			migrationBuilder.AddForeignKey(
				name: "FK_Entries_EntryTextContent_TextContentId",
				table: "Entries",
				column: "TextContentId",
				principalTable: "EntryTextContent",
				principalColumn: "Id",
				onDelete: ReferentialAction.Restrict);

			// Migrate text entries
			migrationBuilder.Sql(
				@"INSERT INTO EntryTextContent (Content) 
				SELECT Content FROM __entries__data__ WHERE FileName IS NULL");
			migrationBuilder.Sql(
				$@"INSERT INTO Entries (Id, Timestamp, TextContentId, Type)
				SELECT e.Id, e.Timestamp, et.Id, {(Int32)EntryType.Text}
					FROM __entries__data__ AS e
					JOIN EntryTextContent AS et
						ON e.Content = et.Content");

			// Migrate file entries
			migrationBuilder.Sql(
				@"INSERT INTO EntryFileContent (FileName) 
				SELECT FileName FROM __entries__data__ WHERE FileName IS NOT NULL");
			migrationBuilder.Sql(
				$@"INSERT INTO Entries (Id, Timestamp, Description, FileContentId, Type)
				SELECT e.Id, e.Timestamp, e.Content, ef.Id, {(Int32)EntryType.File}
					FROM __entries__data__ AS e
					JOIN EntryFileContent AS ef
						ON e.FileName = ef.FileName");

			migrationBuilder.Sql("DROP TABLE IF EXISTS __entries__data__");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.Sql("DROP TABLE IF EXISTS __entries");
			migrationBuilder.Sql("DROP TABLE IF EXISTS __texts");
			migrationBuilder.Sql("DROP TABLE IF EXISTS __files");
			migrationBuilder.Sql("CREATE TABLE __entries AS SELECT * FROM Entries");
			migrationBuilder.Sql("CREATE TABLE __texts AS SELECT * FROM EntryTextContent");
			migrationBuilder.Sql("CREATE TABLE __files AS SELECT * FROM EntryFileContent");

			migrationBuilder.DropTable("Entries");
			migrationBuilder.DropTable("EntryTextContent");
			migrationBuilder.DropTable("EntryFileContent");

			migrationBuilder.CreateTable(
				"Entries",
				table => new {
					Id = table.Column<UInt32>(nullable: false)
						.Annotation("Sqlite:Autoincrement", value: true),
					Timestamp = table.Column<Int64>(nullable: false),
					Content = table.Column<String>(nullable: true),
					FileName = table.Column<String>(nullable: true),
					IsDeleted = table.Column<Boolean>(nullable: false, defaultValue: false),
				},
				constraints: table => {
					table.PrimaryKey("PK_Entries", x => x.Id);
				});

			// Migrate text entries
			migrationBuilder.Sql(
				@"INSERT INTO Entries (Id, Timestamp, Content, IsDeleted)
				SELECT e.Id, e.Timestamp, et.Content, e.IsDeleted
					FROM __entries AS e
					JOIN __texts AS et ON e.TextContentId = et.Id");

			// Migrate file entry descriptions
			migrationBuilder.Sql(
				@"INSERT INTO Entries (Id, Timestamp, Content, FileName, IsDeleted)
				SELECT e.Id, e.Timestamp, e.Description, ef.FileName, e.IsDeleted
					FROM __entries AS e
					JOIN __files AS ef ON e.FileContentId = ef.Id");

			migrationBuilder.Sql("DROP TABLE IF EXISTS __entries");
			migrationBuilder.Sql("DROP TABLE IF EXISTS __texts");
			migrationBuilder.Sql("DROP TABLE IF EXISTS __files");
		}
	}
}