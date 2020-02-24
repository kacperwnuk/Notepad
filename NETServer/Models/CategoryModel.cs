using System.Collections.Generic;
using System.Globalization;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Z05.Models
{
    [Table("Category", Schema = "wnuk")]
    public class Category
    {


        public Category()
        {
            
        }
        public Category(string title, List<NoteCategory> noteCategories)
        {
            Title = title;
            Notes = noteCategories;
        }
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryID { get; set; }

        [MaxLength(64)]
        public string Title { get; set; }
        
        [JsonIgnore]
        public List<NoteCategory> Notes { get; set; }

        public override string ToString()
        {
            return $"{Title}";
        }
    }

}
