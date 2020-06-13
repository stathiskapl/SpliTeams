﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SplitTeam.Model;

namespace SplitTeam.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20200613175015_matches description")]
    partial class matchesdescription
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SplitTeam.Model.Match", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreatedOnDateTime");

                    b.Property<string>("Description");

                    b.Property<int?>("ScoreTeamA");

                    b.Property<int?>("ScoreTeamB");

                    b.Property<int?>("TeamAId");

                    b.Property<int?>("TeamBId");

                    b.Property<DateTime?>("UpdatedOnDateTime");

                    b.HasKey("Id");

                    b.HasIndex("TeamAId");

                    b.HasIndex("TeamBId");

                    b.ToTable("Matches");
                });

            modelBuilder.Entity("SplitTeam.Model.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal?>("AverageRank");

                    b.Property<DateTime?>("CreatedOnDateTime");

                    b.Property<string>("Name");

                    b.Property<DateTime?>("UpdatedOnDateTime");

                    b.HasKey("Id");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("SplitTeam.Model.PlayerRank", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreatedOnDateTime");

                    b.Property<int?>("PlayerId");

                    b.Property<int>("Rank");

                    b.Property<int?>("SkillId");

                    b.Property<DateTime?>("UpdatedOnDateTime");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.HasIndex("SkillId");

                    b.HasIndex("UserId");

                    b.ToTable("PlayerRanks");
                });

            modelBuilder.Entity("SplitTeam.Model.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreatedOnDateTime");

                    b.Property<string>("Name");

                    b.Property<DateTime?>("UpdatedOnDateTime");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("SplitTeam.Model.Skill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreatedOnDateTime");

                    b.Property<string>("Name");

                    b.Property<DateTime?>("UpdatedOnDateTime");

                    b.HasKey("Id");

                    b.ToTable("Skills");
                });

            modelBuilder.Entity("SplitTeam.Model.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreatedOnDateTime");

                    b.Property<string>("Name");

                    b.Property<DateTime?>("UpdatedOnDateTime");

                    b.HasKey("Id");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("SplitTeam.Model.TeamPlayer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreatedOnDateTime");

                    b.Property<int?>("PlayerId");

                    b.Property<int?>("TeamId");

                    b.Property<DateTime?>("UpdatedOnDateTime");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.HasIndex("TeamId");

                    b.ToTable("TeamPlayers");
                });

            modelBuilder.Entity("SplitTeam.Model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreatedOnDateTime");

                    b.Property<string>("Password");

                    b.Property<int?>("RoleId");

                    b.Property<DateTime?>("UpdatedOnDateTime");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SplitTeam.Model.Match", b =>
                {
                    b.HasOne("SplitTeam.Model.Team", "TeamA")
                        .WithMany()
                        .HasForeignKey("TeamAId");

                    b.HasOne("SplitTeam.Model.Team", "TeamB")
                        .WithMany()
                        .HasForeignKey("TeamBId");
                });

            modelBuilder.Entity("SplitTeam.Model.PlayerRank", b =>
                {
                    b.HasOne("SplitTeam.Model.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId");

                    b.HasOne("SplitTeam.Model.Skill", "Skill")
                        .WithMany()
                        .HasForeignKey("SkillId");

                    b.HasOne("SplitTeam.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("SplitTeam.Model.TeamPlayer", b =>
                {
                    b.HasOne("SplitTeam.Model.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId");

                    b.HasOne("SplitTeam.Model.Team", "Team")
                        .WithMany()
                        .HasForeignKey("TeamId");
                });

            modelBuilder.Entity("SplitTeam.Model.User", b =>
                {
                    b.HasOne("SplitTeam.Model.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId");
                });
#pragma warning restore 612, 618
        }
    }
}