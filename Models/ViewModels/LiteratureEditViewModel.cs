using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Portfolio_Strona.Models.ViewModels
{
    public class LiteratureEditViewModel
    {
        public int LiteratureID { get; set; }
        [Required(ErrorMessage = "Please fill it up.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Please fill it up.")]
        public string Authors { get; set; }
        [Required(ErrorMessage = "Please fill it up.")]
        public string Url { get; set; }
        public IEnumerable<SelectListItem> Technologies { get; set; }
        [Required(ErrorMessage = "Please fill it up.")]
        public int[] TechnologyIds { get; set; }
    }
}
