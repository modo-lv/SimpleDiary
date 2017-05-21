using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Diary.Main.Migrations
{
	public partial class EntryFilePathToFileName : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.Sql("DROP TABLE IF EXISTS __entries__data__");
			migrationBuilder.Sql("CREATE TABLE __entries__data__ AS SELECT * FROM Entries");

			migrationBuilder.DropTable("Entries");

			migrationBuilder.CreateTable(
				"Entries",
				table => new
				{
					Id = table.Column<UInt32>(nullable: false)
						.Annotation("Sqlite:Autoincrement", value: true),
					Timestamp = table.Column<Int64>(nullable: false),
					Content = table.Column<String>(nullable: true),
					FileName = table.Column<String>(nullable: true),
					IsDeleted = table.Column<Boolean>(nullable: false, defaultValue: false),
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Entries", x => x.Id);
				});

			migrationBuilder.Sql(
				"INSERT INTO Entries (Id, Timestamp, Content, IsDeleted, FileName) " +
				"SELECT Id, Timestamp, Content, IsDeleted, FilePath FROM __entries__data__");
			migrationBuilder.Sql("DROP TABLE IF EXISTS __entries__data__");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.Sql("DROP TABLE IF EXISTS __entries__data__");
			migrationBuilder.Sql("CREATE TABLE __entries__data__ AS SELECT * FROM Entries");

			migrationBuilder.DropTable("Entries");

			migrationBuilder.CreateTable(
				"Entries",
				table => new
				{
					Id = table.Column<UInt32>(nullable: false)
						.Annotation("Sqlite:Autoincrement", value: true),
					Timestamp = table.Column<Int64>(nullable: false),
					Content = table.Column<String>(nullable: true),
					FilePath = table.Column<String>(nullable: true),
					IsDeleted = table.Column<Boolean>(nullable: false, defaultValue: false),
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Entries", x => x.Id);
				});

			migrationBuilder.Sql(
				"INSERT INTO Entries (Id, Timestamp, Content, IsDeleted, FilePath) " +
				"SELECT Id, Timestamp, Content, IsDeleted, FileName FROM __entries__data__");
			migrationBuilder.Sql("DROP TABLE IF EXISTS __entries__data__");
		}
	}
}
