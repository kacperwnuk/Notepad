using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;


namespace Z05.Models
{
    public class NotepadContext : DbContext
    {
        public NotepadContext(DbContextOptions<NotepadContext> options) : base(options)
        {
        }

        public DbSet<Note> Notes { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Category>()
                .HasIndex(c => c.Title)
                .IsUnique();

            builder.Entity<NoteCategory>()
                .HasKey(t => new {t.NoteID, t.CategoryID});

            builder.Entity<NoteCategory>()
                .HasOne(nc => nc.Category)
                .WithMany(n => n.Notes)
                .HasForeignKey(nc => nc.CategoryID);

            builder.Entity<NoteCategory>()
                .HasOne(nc => nc.Note)
                .WithMany(n => n.Categories)
                .HasForeignKey(nc => nc.NoteID);

            builder.Entity<Category>().HasData(
                new Category {CategoryID = 1, Title = "zadanie"},
                new Category {CategoryID = 2, Title = "sprawko"},
                new Category {CategoryID = 3, Title = "informatyka"},
                new Category {CategoryID = 4, Title = "zakupy"},
                new Category {CategoryID = 5, Title = "impreza"},
                new Category {CategoryID = 6, Title = "warzywa"}
            );

            builder.Entity<Note>().HasData(
                new Note
                {
                    NoteID = 1, Description = "Pierwsza notatka", Title = "Zakupy", NoteDate = DateTime.Now,
                    IsMarkdownFile = false
                },
                new Note
                {
                    NoteID = 2, Description = "Druga notatka", Title = "Zabieg", NoteDate = DateTime.Now,
                    IsMarkdownFile = false
                },
                new Note
                {
                    NoteID = 3, Description = "Trzecia notatka", Title = "Wyjazd", NoteDate = DateTime.Now,
                    IsMarkdownFile = false
                },
                new Note
                {
                    NoteID = 4, Description = "Czwarta notatka", Title = "Misja", NoteDate = DateTime.Now,
                    IsMarkdownFile = false
                }, new Note
                {
                    NoteID = 5, Description = "Kolejna notatka", Title = "Szkolenie", NoteDate = DateTime.Now,
                    IsMarkdownFile = false
                }, new Note
                {
                    NoteID = 6, Description = "Zadanie domowe", Title = "Wycieczka", NoteDate = DateTime.Now,
                    IsMarkdownFile = false
                }
            );

            builder.Entity<NoteCategory>().HasData(
                new NoteCategory {CategoryID = 1, NoteID = 1},
                new NoteCategory {CategoryID = 2, NoteID = 2},
                new NoteCategory {CategoryID = 2, NoteID = 3},
                new NoteCategory {CategoryID = 3, NoteID = 3},
                new NoteCategory {CategoryID = 4, NoteID = 4},
                new NoteCategory {CategoryID = 5, NoteID = 5},
                new NoteCategory {CategoryID = 6, NoteID = 6}
            );
            
            base.OnModelCreating(builder);
        }
    }
}