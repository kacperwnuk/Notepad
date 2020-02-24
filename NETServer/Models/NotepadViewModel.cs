using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Z05.Models
{
    public class NotepadViewModel
    {
        public IEnumerable<Note> Notes { get; set; }
        public IEnumerable<Category> Categories { get; set; }

        public IEnumerable<SelectListItem> CategoryItems {
            get
            {
                var allFlavors = Categories.Select(f => new SelectListItem
                {
                    Value = f.CategoryID.ToString(),
                    Text = f.Title           
                });
                return allFlavors;
         
            }    
        }
    }
}