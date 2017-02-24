using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Diary.Main.Core.Persistence;

namespace Diary.Main.Migrations
{
    [DbContext(typeof(DiaryDbContext))]
    partial class DiaryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

            modelBuilder.Entity("Diary.Main.Domain.Entities.Entry", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Entries");
                });

            modelBuilder.Entity("Diary.Main.Domain.Entities.EntryTimestamp", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<uint?>("EntryId");

                    b.Property<ulong>("Timestamp");

                    b.HasKey("Id");

                    b.HasIndex("EntryId");

                    b.ToTable("EntryTimestamp");
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
