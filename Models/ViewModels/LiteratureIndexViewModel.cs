using System.Collections.Generic;

namespace Portfolio_Strona.Models.ViewModels
{
    public class LiteratureIndexViewModel
    {
        public IEnumerable<Technology> TechnologyList { get; set; }
        public IEnumerable<Literature> LiteratureList { get; set; }
        public PagingInfoViewModel PagingInfo { get; set; }
    }
}
