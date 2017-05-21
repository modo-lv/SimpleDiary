﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Diary.Main.Core.Persistence;

namespace Diary.Main.Migrations
{
    [DbContext(typeof(DiaryDbContext))]
    [Migration("20170521122810_EntryFilePathToFileName")]
    partial class EntryFilePathToFileName
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("Diary.Main.Domain.Entities.Entry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<string>("FileName");

                    b.Property<bool>("IsDeleted");

                    b.Property<long>("Timestamp");

                    b.HasKey("Id");

                    b.ToTable("Entries");
                });
        }
    }
}
