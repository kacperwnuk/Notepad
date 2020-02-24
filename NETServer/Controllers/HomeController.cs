using System.IO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Z05.Models;

namespace Z05.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Components.Route("")]
    public class HomeController : Controller
    {
        private readonly NotepadContext _context;

        public HomeController(NotepadContext context)
        {
            _context = context;
        }
        
        
        public class CategoriesObject
        {
            public IEnumerable<Category> Categories { get; set; }
        }

        public class NotesObject
        {
            public IEnumerable<Note> Notes { get; set; }
            public int Total { get; set; }
        }

        public class MessageObject
        {
            public string Message { get; set; }
        }
        
        
        [HttpGet("/home")]
        public ActionResult<NotesObject> Index(int? category, string? from, string? to, int? page, int? pageSize)
        {
            try
            {
                var format = "yyyy-MM-dd";
                var filter = new Filter
                {
                    Page = page,
                    PageSize = pageSize,
                    Category = category,
                    From = from == null ? DateTime.MinValue : DateTime.ParseExact(from, format, CultureInfo.InvariantCulture),
                    To = to == null ? DateTime.MinValue : DateTime.ParseExact(to, format, CultureInfo.InvariantCulture)
                };
                return FilterNotes(filter);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpGet("/categories")]
        public ActionResult<CategoriesObject> GetCategories()
        {
            var categories = _context.Categories;
            return new CategoriesObject { Categories = categories};
        }
        
 
        [HttpGet("/note/{id}")]
        public ActionResult<Note> NoteEditor(int id)
        {
            var note = _context.Notes.Include(c => c.Categories).ThenInclude(n => n.Category).SingleOrDefault(n => n.NoteID == id);
            return note;
        }

        [HttpPost("/categories")]
        public ActionResult<Category> AddCategory(Category category)
        {
            
            try
            {
                if (category.CategoryID != 0 || category.Title == null)
                {
                    return BadRequest();
                }
                _context.Categories.Add(category);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
            
            return _context.Categories.SingleOrDefault(c => c.Title.Equals(category.Title));
        }
        
        [HttpPost("/note")]
        public ActionResult<string> AddNote(Note note)
        {
            try
            {
                ValidateNote(note);
                var categories = note.Categories.ToList();
                note.Categories.Clear();
                _context.Attach(note);
                foreach(var category in categories)
                {
                    note.Categories.Add(new NoteCategory {
                        NoteID = note.NoteID,
                        Category = _context.Categories.Find(category.CategoryID)
                    });
                }
                _context.Add(note);
                _context.SaveChanges();
                return new ActionResult<string>("Note added.");   
            }catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpPut("/note")]
        public ActionResult<MessageObject> UpdateNote(Note note)
        {
            try
            {
                TrySaveNote(note);
                return new MessageObject{Message = "Note updated"};   
            }catch (Exception e)
            {
                return BadRequest(new MessageObject{Message="Someone else edited or deleted this note!"});
            }
        }

        [HttpDelete("/note/{id}")]
        public ActionResult<MessageObject> DeleteNote(int id)
        {
            var note = _context.Notes.Find(id);
            if (note != null)
            {
                _context.Notes.Remove(note);
                _context.SaveChanges();
            }
            else
            {
                return NotFound();
            }
            
            return new MessageObject{Message = "Note deleted"};
        }
        
        private NotesObject FilterNotes(Filter filter)
        {
            IQueryable<Note> notesToShow = _context.Notes;

            if (filter.Category != null)
            {
                notesToShow = notesToShow.Where(note => note.Categories.Any(c => c.CategoryID == filter.Category));
            }
            
            if (filter.From.CompareTo(DateTime.MinValue) > 0)
            {
                notesToShow = notesToShow.Where(note => note.NoteDate.CompareTo(filter.From) >= 0);
            }
            
            if (filter.To.CompareTo(DateTime.MinValue) > 0)
            {
                notesToShow = notesToShow.Where(note => note.NoteDate.CompareTo(filter.To) <= 0);
            }

            var totalNotes = notesToShow.Count();
            
            if (filter.Page != null && filter.PageSize != null)
            {
                var start = (filter.Page - 1) * filter.PageSize;
                filter.PageSize = start + filter.PageSize < totalNotes ? filter.PageSize : totalNotes - start;
                if (filter.PageSize <= 0)
                {
                    return new NotesObject {Notes = new List<Note>(), Total = 0};
                }
                notesToShow = notesToShow.Skip((int)start).Take((int)filter.PageSize);
            }

            return new NotesObject{Notes = notesToShow, Total = totalNotes};
        }
        
        private void TrySaveNote(Note note)
        {
            var dbNote = _context.Notes.Include(n => n.Categories).SingleOrDefault(n => n.NoteID == note.NoteID) ??
                         note;
            dbNote.Title = note.Title;
            dbNote.IsMarkdownFile = note.IsMarkdownFile;
            dbNote.Description = note.Description;
            dbNote.NoteDate = note.NoteDate;
            
            var categories = note.Categories.ToList();
            note.Categories.Clear();
            foreach(var category in categories)
            {
                note.Categories.Add(new NoteCategory {
                    NoteID = note.NoteID,
                    Category = _context.Categories.Find(category.CategoryID)
                });
            }
            
            dbNote.Categories = note.Categories;
            _context.Entry(dbNote).Property("RowVersion").OriginalValue = note.RowVersion;
            _context.Update(dbNote);
            _context.SaveChanges();

        }
        

        private void ValidateNote(Note note)
        {
            if (note.Title == null)
            {
                throw new ArgumentException("Note must have title!");
            }
        }

//        
//        [HttpPost]
//        public IActionResult NoteEditor(Note note, List<string> categories)
//        {
//            if (ModelState.IsValid)
//            {
//                UploadCategories(note, categories);
//                TrySaveNote(note);
//                try
//                {
//                    _context.SaveChanges();
//                    return RedirectToAction("Index", "Home", new {noteId = note.NoteID});    
//                }
//                catch (DbUpdateConcurrencyException ex)
//                {
//                    var exceptionEntry = ex.Entries.Single();
//                    var clientValues = (Note)exceptionEntry.Entity;
//                    var databaseEntry = exceptionEntry.GetDatabaseValues();
//                    if (databaseEntry == null)
//                    {
//                        ViewData["ExceptionMessage"] =
//                            "Someone deleted these note during your modification, you can still create new note.";
//                        ModelState.Remove("NoteID");
//                        ModelState.Remove("RowVersion");
//                        note.NoteID = 0;
//                    }
//                    else
//                    {
//                        var dbNote = (Note) databaseEntry.ToObject();
//                        if (!dbNote.Title.Equals(clientValues.Title))
//                        {
//                            ModelState.AddModelError("Title", $"Current value: {dbNote.Title}");
//                        }
//                        if (!dbNote.Description.Equals(clientValues.Description))
//                        {
//                            ModelState.AddModelError("Description", $"Current value: {dbNote.Description}");
//                        }
//                        if (dbNote.NoteDate != clientValues.NoteDate)
//                        {
//                            ModelState.AddModelError("NoteDate", $"Current value: {dbNote.NoteDate}");
//                        }
//                        
//                        note.RowVersion = dbNote.RowVersion;
//                        ModelState.Remove("RowVersion");
//                        ViewData["ExceptionMessage"] =
//                            "Someone modified these note during your modification, please edit note once again.";
//                    }
//
//                    return View("NoteEditor", note);
//                }
//            }
//            return View("NoteEditor", note);
//        }

//        [HttpPost]
//        public IActionResult AddCategory(Note note, string categoryTitle, List<string> categories)
//        {
//            UploadCategories(note, categories);
//            if (categoryTitle != null)
//            {
//                var existingCategory = _context.Categories.SingleOrDefault(c => c.Title.Equals(categoryTitle));
//                if (existingCategory != null)
//                {
//                    if (!note.Categories.Any(nc =>
//                        nc.NoteID == note.NoteID && nc.CategoryID == existingCategory.CategoryID))
//                    {
//                        note.Categories.Add(new NoteCategory
//                        {
//                            NoteID = note.NoteID,
//                            CategoryID = existingCategory.CategoryID,
//                            Category = existingCategory
//                        });
//                    }
//                }
//                else
//                {
//                    var category = new Category {Title = categoryTitle};
//                    note.Categories.Add(new NoteCategory
//                    {
//                        NoteID = note.NoteID,
//                        Category = category
//                    });
//                }
//            }
//
//            return View("NoteEditor", note);
//        }
//

     
        
    }
}