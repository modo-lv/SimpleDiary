using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Diary.Main.Core.Persistence;

namespace Diary.Main.Migrations
{
    [DbContext(typeof(DiaryDbContext))]
    [Migration("20170514152313_EntryTimestampTableName")]
    partial class EntryTimestampTableName
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("Diary.Main.Domain.Entities.Entry", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Entries");
                });

            modelBuilder.Entity("Diary.Main.Domain.Entities.EntryTimestamp", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<uint?>("EntryId");

                    b.Property<long>("Timestamp");

                    b.HasKey("Id");

                    b.HasIndex("EntryId");

                    b.ToTable("EntryTimestamps");
                });

            modelBuilder.Entity("Diary.Main.Domain.Entities.EntryTimestamp", b =>
                {
                    b.HasOne("Diary.Main.Domain.Entities.Entry", "Entry")
                        .WithMany("Timestamps")
                        .HasForeignKey("EntryId");
                });
        }
    }
}
