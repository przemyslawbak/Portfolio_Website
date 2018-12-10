using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Portfolio_Strona.Models.ViewModels
{
    public class ProjectViewModel
    {
        public int ProjectID { get; set; }
        [Required(ErrorMessage = "Please fill it up.")]
        [StringLength(20, ErrorMessage = "Max 20 characters.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please fill it up.")]
        [StringLength(7, ErrorMessage = "Max 7 characters.")]
        public string BackColor { get; set; }
        public string Comments { get; set; }
        public string GitHubLink { get; set; }
        public string WebLink { get; set; }
        [Required(ErrorMessage = "Please fill it up.")]
        [DataType(DataType.Date)]
        public DateTime CompletionDate { get; set; }
        //dropdown
        public IEnumerable<SelectListItem> Technologies { get; set; }
        [Required(ErrorMessage = "Please fill it up.")]
        public int[] TechnologyIds { get; set; }
        public string PictureUrl { get; set; }
        public string WorkLogUrl { get; set; }
        public string YouTubeUrl { get; set; }
    }
}
