﻿// <auto-generated />
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
    [Migration("20230116102311_new-models-7")]
    partial class newmodels7
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CardDeck", b =>
                {
                    b.Property<int>("DecksId")
                        .HasColumnType("int");

                    b.Property<int>("CardsSymbol")
                        .HasColumnType("int");

                    b.Property<int>("CardsType")
                        .HasColumnType("int");

                    b.HasKey("DecksId", "CardsSymbol", "CardsType");

                    b.HasIndex("CardsSymbol", "CardsType");

                    b.ToTable("CardDeck");
                });

            modelBuilder.Entity("CardPlayer", b =>
                {
                    b.Property<int>("PlayersId")
                        .HasColumnType("int");

                    b.Property<int>("CardsSymbol")
                        .HasColumnType("int");

                    b.Property<int>("CardsType")
                        .HasColumnType("int");

                    b.HasKey("PlayersId", "CardsSymbol", "CardsType");

                    b.HasIndex("CardsSymbol", "CardsType");

                    b.ToTable("CardPlayer");
                });

            modelBuilder.Entity("CardPokerTable", b =>
                {
                    b.Property<int>("TablesId")
                        .HasColumnType("int");

                    b.Property<int>("CardsSymbol")
                        .HasColumnType("int");

                    b.Property<int>("CardsType")
                        .HasColumnType("int");

                    b.HasKey("TablesId", "CardsSymbol", "CardsType");

                    b.HasIndex("CardsSymbol", "CardsType");

                    b.ToTable("CardPokerTable");
                });

            modelBuilder.Entity("GameEngine.Models.Game.Card", b =>
                {
                    b.Property<int>("Symbol")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Symbol", "Type");

                    b.ToTable("Card");
                });

            modelBuilder.Entity("GameEngine.Models.Game.Deck", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.HasKey("Id");

                    b.ToTable("Deck");
                });

            modelBuilder.Entity("GameEngine.Models.Game.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Chips")
                        .HasColumnType("int");

                    b.Property<int>("CurrentBet")
                        .HasColumnType("int");

                    b.Property<bool>("IsFolded")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Player");
                });

            modelBuilder.Entity("GameEngine.Models.Game.PokerTable", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Chips")
                        .HasColumnType("int");

                    b.Property<int>("DeckId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DeckId");

                    b.ToTable("Table");
                });

            modelBuilder.Entity("PlayerPokerTable", b =>
                {
                    b.Property<int>("PlayersId")
                        .HasColumnType("int");

                    b.Property<int>("TablesId")
                        .HasColumnType("int");

                    b.HasKey("PlayersId", "TablesId");

                    b.HasIndex("TablesId");

                    b.ToTable("PlayerPokerTable");
                });

            modelBuilder.Entity("CardDeck", b =>
                {
                    b.HasOne("GameEngine.Models.Game.Deck", null)
                        .WithMany()
                        .HasForeignKey("DecksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GameEngine.Models.Game.Card", null)
                        .WithMany()
                        .HasForeignKey("CardsSymbol", "CardsType")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CardPlayer", b =>
                {
                    b.HasOne("GameEngine.Models.Game.Player", null)
                        .WithMany()
                        .HasForeignKey("PlayersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GameEngine.Models.Game.Card", null)
                        .WithMany()
                        .HasForeignKey("CardsSymbol", "CardsType")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CardPokerTable", b =>
                {
                    b.HasOne("GameEngine.Models.Game.PokerTable", null)
                        .WithMany()
                        .HasForeignKey("TablesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GameEngine.Models.Game.Card", null)
                        .WithMany()
                        .HasForeignKey("CardsSymbol", "CardsType")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GameEngine.Models.Game.PokerTable", b =>
                {
                    b.HasOne("GameEngine.Models.Game.Deck", "Deck")
                        .WithMany()
                        .HasForeignKey("DeckId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Deck");
                });

            modelBuilder.Entity("PlayerPokerTable", b =>
                {
                    b.HasOne("GameEngine.Models.Game.Player", null)
                        .WithMany()
                        .HasForeignKey("PlayersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GameEngine.Models.Game.PokerTable", null)
                        .WithMany()
                        .HasForeignKey("TablesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
