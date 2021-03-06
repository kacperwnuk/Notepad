﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Z05.Models;

namespace Z05.Migrations
{
    [DbContext(typeof(NotepadContext))]
    partial class NotepadContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Z05.Models.Category", b =>
                {
                    b.Property<int>("CategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(64)")
                        .HasMaxLength(64);

                    b.HasKey("CategoryID");

                    b.HasIndex("Title")
                        .IsUnique()
                        .HasFilter("[Title] IS NOT NULL");

                    b.ToTable("Category","wnuk");

                    b.HasData(
                        new
                        {
                            CategoryID = 1,
                            Title = "zadanie"
                        },
                        new
                        {
                            CategoryID = 2,
                            Title = "sprawko"
                        },
                        new
                        {
                            CategoryID = 3,
                            Title = "informatyka"
                        },
                        new
                        {
                            CategoryID = 4,
                            Title = "zakupy"
                        },
                        new
                        {
                            CategoryID = 5,
                            Title = "impreza"
                        },
                        new
                        {
                            CategoryID = 6,
                            Title = "warzywa"
                        });
                });

            modelBuilder.Entity("Z05.Models.Note", b =>
                {
                    b.Property<int>("NoteID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsMarkdownFile")
                        .HasColumnType("bit");

                    b.Property<DateTime>("NoteDate")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(64)")
                        .HasMaxLength(64);

                    b.HasKey("NoteID");

                    b.ToTable("Note","wnuk");

                    b.HasData(
                        new
                        {
                            NoteID = 1,
                            Description = "Pierwsza notatka",
                            IsMarkdownFile = false,
                            NoteDate = new DateTime(2019, 11, 21, 14, 9, 37, 431, DateTimeKind.Local).AddTicks(7864),
                            Title = "Zakupy"
                        },
                        new
                        {
                            NoteID = 2,
                            Description = "Druga notatka",
                            IsMarkdownFile = false,
                            NoteDate = new DateTime(2019, 11, 21, 14, 9, 37, 436, DateTimeKind.Local).AddTicks(280),
                            Title = "Zabieg"
                        },
                        new
                        {
                            NoteID = 3,
                            Description = "Trzecia notatka",
                            IsMarkdownFile = false,
                            NoteDate = new DateTime(2019, 11, 21, 14, 9, 37, 436, DateTimeKind.Local).AddTicks(331),
                            Title = "Wyjazd"
                        },
                        new
                        {
                            NoteID = 4,
                            Description = "Czwarta notatka",
                            IsMarkdownFile = false,
                            NoteDate = new DateTime(2019, 11, 21, 14, 9, 37, 436, DateTimeKind.Local).AddTicks(337),
                            Title = "Misja"
                        },
                        new
                        {
                            NoteID = 5,
                            Description = "Kolejna notatka",
                            IsMarkdownFile = false,
                            NoteDate = new DateTime(2019, 11, 21, 14, 9, 37, 436, DateTimeKind.Local).AddTicks(340),
                            Title = "Szkolenie"
                        },
                        new
                        {
                            NoteID = 6,
                            Description = "Zadanie domowe",
                            IsMarkdownFile = false,
                            NoteDate = new DateTime(2019, 11, 21, 14, 9, 37, 436, DateTimeKind.Local).AddTicks(343),
                            Title = "Wycieczka"
                        });
                });

            modelBuilder.Entity("Z05.Models.NoteCategory", b =>
                {
                    b.Property<int>("NoteID")
                        .HasColumnType("int");

                    b.Property<int>("CategoryID")
                        .HasColumnType("int");

                    b.HasKey("NoteID", "CategoryID");

                    b.HasIndex("CategoryID");

                    b.ToTable("NoteCategory","wnuk");

                    b.HasData(
                        new
                        {
                            NoteID = 1,
                            CategoryID = 1
                        },
                        new
                        {
                            NoteID = 2,
                            CategoryID = 2
                        },
                        new
                        {
                            NoteID = 3,
                            CategoryID = 2
                        },
                        new
                        {
                            NoteID = 3,
                            CategoryID = 3
                        },
                        new
                        {
                            NoteID = 4,
                            CategoryID = 4
                        },
                        new
                        {
                            NoteID = 5,
                            CategoryID = 5
                        },
                        new
                        {
                            NoteID = 6,
                            CategoryID = 6
                        });
                });

            modelBuilder.Entity("Z05.Models.NoteCategory", b =>
                {
                    b.HasOne("Z05.Models.Category", "Category")
                        .WithMany("Notes")
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Z05.Models.Note", "Note")
                        .WithMany("Categories")
                        .HasForeignKey("NoteID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
