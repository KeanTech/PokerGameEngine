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
    [Migration("20230113082917_mdsalkdmksladsddwqq222323222")]
    partial class mdsalkdmksladsddwqq222323222
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("GameEngine.Data.PlayerCard", b =>
                {
                    b.Property<int>("CardId")
                        .HasColumnType("int");

                    b.Property<int>("PlayerId")
                        .HasColumnType("int");

                    b.HasKey("CardId", "PlayerId");

                    b.HasIndex("PlayerId");

                    b.ToTable("PlayerCards");
                });

            modelBuilder.Entity("GameEngine.Models.Game.Card", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Symbol")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Card");
                });

            modelBuilder.Entity("GameEngine.Models.Game.DeckCard", b =>
                {
                    b.Property<int>("CardId")
                        .HasColumnType("int");

                    b.Property<int>("TableId")
                        .HasColumnType("int");

                    b.HasKey("CardId", "TableId");

                    b.HasIndex("TableId");

                    b.ToTable("DeckCards");
                });

            modelBuilder.Entity("GameEngine.Models.Game.Gametable", b =>
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

            modelBuilder.Entity("GameEngine.Models.Game.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Chips")
                        .HasColumnType("int");

                    b.Property<int>("CurrentBet")
                        .HasColumnType("int");

                    b.Property<int?>("GametableId")
                        .HasColumnType("int");

                    b.Property<bool>("IsFolded")
                        .HasColumnType("bit");

                    b.Property<int>("Name")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GametableId");

                    b.ToTable("Player");
                });

            modelBuilder.Entity("GameEngine.Models.Game.TableCard", b =>
                {
                    b.Property<int>("CardId")
                        .HasColumnType("int");

                    b.Property<int>("TableId")
                        .HasColumnType("int");

                    b.HasKey("CardId", "TableId");

                    b.HasIndex("TableId");

                    b.ToTable("TableCards");
                });

            modelBuilder.Entity("GameEngine.Data.PlayerCard", b =>
                {
                    b.HasOne("GameEngine.Models.Game.Card", "Card")
                        .WithMany("Players")
                        .HasForeignKey("CardId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("GameEngine.Models.Game.Player", "Player")
                        .WithMany("Cards")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Card");

                    b.Navigation("Player");
                });

            modelBuilder.Entity("GameEngine.Models.Game.DeckCard", b =>
                {
                    b.HasOne("GameEngine.Models.Game.Card", "Card")
                        .WithMany("Decks")
                        .HasForeignKey("CardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GameEngine.Models.Game.Gametable", "Table")
                        .WithMany("CardDeck")
                        .HasForeignKey("TableId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Card");

                    b.Navigation("Table");
                });

            modelBuilder.Entity("GameEngine.Models.Game.Player", b =>
                {
                    b.HasOne("GameEngine.Models.Game.Gametable", null)
                        .WithMany("Players")
                        .HasForeignKey("GametableId");
                });

            modelBuilder.Entity("GameEngine.Models.Game.TableCard", b =>
                {
                    b.HasOne("GameEngine.Models.Game.Card", "Card")
                        .WithMany("Tables")
                        .HasForeignKey("CardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GameEngine.Models.Game.Gametable", "Gametable")
                        .WithMany("Cards")
                        .HasForeignKey("TableId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Card");

                    b.Navigation("Gametable");
                });

            modelBuilder.Entity("GameEngine.Models.Game.Card", b =>
                {
                    b.Navigation("Decks");

                    b.Navigation("Players");

                    b.Navigation("Tables");
                });

            modelBuilder.Entity("GameEngine.Models.Game.Gametable", b =>
                {
                    b.Navigation("CardDeck");

                    b.Navigation("Cards");

                    b.Navigation("Players");
                });

            modelBuilder.Entity("GameEngine.Models.Game.Player", b =>
                {
                    b.Navigation("Cards");
                });
#pragma warning restore 612, 618
        }
    }
}
