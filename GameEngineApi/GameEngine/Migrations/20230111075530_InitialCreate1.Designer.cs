﻿// <auto-generated />
using System;
using GameEngine.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GameEngine.Migrations
{
    [DbContext(typeof(GameEngineContext))]
    [Migration("20230111075530_InitialCreate1")]
    partial class InitialCreate1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("GameEngine.Models.Game.Accessory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Accessory");
                });

            modelBuilder.Entity("GameEngine.Models.Game.Card", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("PlayerId")
                        .HasColumnType("int");

                    b.Property<int>("Symbol")
                        .HasColumnType("int");

                    b.Property<int?>("TableId")
                        .HasColumnType("int");

                    b.Property<int?>("TableId1")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.HasIndex("TableId");

                    b.HasIndex("TableId1");

                    b.ToTable("Card");
                });

            modelBuilder.Entity("GameEngine.Models.Game.Chip", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Chip");
                });

            modelBuilder.Entity("GameEngine.Models.Game.Table", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Chips")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Table");
                });

            modelBuilder.Entity("GameEngine.Models.Game.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ChipsAquired")
                        .HasColumnType("int");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Name")
                        .HasColumnType("int");

                    b.Property<int>("TableId")
                        .HasColumnType("int");

                    b.Property<string>("UserIdentifier")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Wins")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("User");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");
                });

            modelBuilder.Entity("GameEngine.Models.Game.Player", b =>
                {
                    b.HasBaseType("GameEngine.Models.Game.User");

                    b.Property<int>("Chips")
                        .HasColumnType("int");

                    b.Property<int>("CurrentBet")
                        .HasColumnType("int");

                    b.HasIndex("TableId");

                    b.HasDiscriminator().HasValue("Player");
                });

            modelBuilder.Entity("GameEngine.Models.Game.Accessory", b =>
                {
                    b.HasOne("GameEngine.Models.Game.User", null)
                        .WithMany("Accessories")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GameEngine.Models.Game.Card", b =>
                {
                    b.HasOne("GameEngine.Models.Game.Player", null)
                        .WithMany("Cards")
                        .HasForeignKey("PlayerId");

                    b.HasOne("GameEngine.Models.Game.Table", null)
                        .WithMany("CardDeck")
                        .HasForeignKey("TableId");

                    b.HasOne("GameEngine.Models.Game.Table", null)
                        .WithMany("Cards")
                        .HasForeignKey("TableId1");
                });

            modelBuilder.Entity("GameEngine.Models.Game.Player", b =>
                {
                    b.HasOne("GameEngine.Models.Game.Table", null)
                        .WithMany("Players")
                        .HasForeignKey("TableId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GameEngine.Models.Game.Table", b =>
                {
                    b.Navigation("CardDeck");

                    b.Navigation("Cards");

                    b.Navigation("Players");
                });

            modelBuilder.Entity("GameEngine.Models.Game.User", b =>
                {
                    b.Navigation("Accessories");
                });

            modelBuilder.Entity("GameEngine.Models.Game.Player", b =>
                {
                    b.Navigation("Cards");
                });
#pragma warning restore 612, 618
        }
    }
}
