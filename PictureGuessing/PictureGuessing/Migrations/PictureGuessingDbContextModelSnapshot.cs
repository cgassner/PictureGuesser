﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PictureGuessing.Models;

namespace PictureGuessing.Migrations
{
    [DbContext(typeof(PictureGuessingDbContext))]
    partial class PictureGuessingDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("PictureGuessing.Models.Difficulty", b =>
                {
                    b.Property<float>("DifficultyScale")
                        .HasColumnType("float");

                    b.Property<int>("cols")
                        .HasColumnType("int");

                    b.Property<float>("revealDelay")
                        .HasColumnType("float");

                    b.Property<int>("rows")
                        .HasColumnType("int");

                    b.HasKey("DifficultyScale");

                    b.ToTable("Difficulties");
                });

            modelBuilder.Entity("PictureGuessing.Models.Game", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Category")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<float?>("DifficultyScale")
                        .HasColumnType("float");

                    b.Property<DateTime>("Endtime")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("isFinished")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid>("pictureID")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("DifficultyScale");

                    b.ToTable("Game");
                });

            modelBuilder.Entity("PictureGuessing.Models.LeaderboardEntry", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Category")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<float>("DifficultyScale")
                        .HasColumnType("float");

                    b.Property<Guid>("GameId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<double>("TimeInSeconds")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.ToTable("LeaderboardEntries");
                });

            modelBuilder.Entity("PictureGuessing.Models.Picture", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Answer")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Category")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("URL")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Pictures");
                });

            modelBuilder.Entity("PictureGuessing.Models.Game", b =>
                {
                    b.HasOne("PictureGuessing.Models.Difficulty", "Difficulty")
                        .WithMany()
                        .HasForeignKey("DifficultyScale");
                });
#pragma warning restore 612, 618
        }
    }
}
