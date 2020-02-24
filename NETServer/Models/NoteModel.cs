using System.Globalization;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Z05.Models
{
    [Table("Note", Schema = "wnuk")]
    public class Note
    {

        public Note()
        {
            this.Categories = new List<NoteCategory>();
        }

//        public Note(DateTime date)
//        {
//            this.Categories = new List<NoteCategory>();
//            this.NoteDate = date;
//        }
//
//        public Note(string title, string content, List<NoteCategory> categories, DateTime date, bool markdownFile)
//        {
//            this.Title = title;
//            this.Description = content;
//            this.Categories = categories;
//            this.NoteDate = date;
//            this.IsMarkdownFile = markdownFile;
//        }


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NoteID { get; set; }

        [Required(ErrorMessage = "Please enter the title of note.")]
        [DisplayName("Title: ")]
        [MaxLength(64)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please fill the content.")]
        [DisplayName("Note content: ")]
        [MaxLength]
        public string Description { get; set; }

        public List<NoteCategory> Categories { get; set; }

        [Required(ErrorMessage = "Please choose a date.")]
        [DisplayName("Date: ")]
        [DisplayFormat(DataFormatString = "{0: yyyy/MM/dd}")]
        public DateTime NoteDate { get; set; }
        
        [DisplayName("Markdown")]
        public bool IsMarkdownFile { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
        
        public override string ToString()
        {
            return "category: " + string.Join(", ", this.Categories)
                    + "\ndate: " + this.NoteDate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture)
                    + "\ncontent: " + this.Description;
        }
    }

    [Table("NoteCategory", Schema="wnuk")]
    public class NoteCategory
    {
        [JsonIgnore]
        public int NoteID { get; set; }
        [JsonIgnore]
        public Note Note { get; set; }
        
        public int CategoryID { get; set; }
        public Category Category { get; set; }
        
        public override string ToString() => $"{Category.Title}";
    }

}
