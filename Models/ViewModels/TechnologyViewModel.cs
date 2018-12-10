using System.Collections.Generic;

namespace Portfolio_Strona.Models.ViewModels
{
    public class TechnologyViewModel
    {
        public IEnumerable<Technology> Technologies { get; set; }
        public IEnumerable<Project> Projects { get; set; }
        public IEnumerable<Literature> Literatures { get; set; }
        public PagingInfoViewModel PagingInfo { get; set; }
    }
}
