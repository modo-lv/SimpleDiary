using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Diary.Main.Core.Persistence;
using Diary.Main.Domain.Entities;

namespace Diary.Main.Migrations
{
    [DbContext(typeof(DiaryDbContext))]
    [Migration("20170522094105_SplitEntryTypes")]
    partial class SplitEntryTypes
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("Diary.Main.Domain.Entities.Entry", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<uint?>("FileContentId");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name");

                    b.Property<uint?>("TextContentId");

                    b.Property<long>("Timestamp");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("FileContentId");

                    b.HasIndex("TextContentId");

                    b.ToTable("Entries");
                });

            modelBuilder.Entity("Diary.Main.Domain.Entities.EntryFileContent", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FileName");

                    b.Property<int>("FileType");

                    b.HasKey("Id");

                    b.ToTable("EntryFileContent");
                });

            modelBuilder.Entity("Diary.Main.Domain.Entities.EntryTextContent", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<int>("Format");

                    b.HasKey("Id");

                    b.ToTable("EntryTextContent");
                });

            modelBuilder.Entity("Diary.Main.Domain.Entities.Entry", b =>
                {
                    b.HasOne("Diary.Main.Domain.Entities.EntryFileContent", "FileContent")
                        .WithMany()
                        .HasForeignKey("FileContentId");

                    b.HasOne("Diary.Main.Domain.Entities.EntryTextContent", "TextContent")
                        .WithMany()
                        .HasForeignKey("TextContentId");
                });
        }
    }
}
