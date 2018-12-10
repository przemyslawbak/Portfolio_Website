using System.Collections.Generic;
using Portfolio_Strona.Models.ViewModels;

namespace Portfolio_Strona.Models
{
    public interface ILiteratureRepository
    {
        IEnumerable<Literature> Literatures { get; }
        Literature DeleteLiterature(int literatureID);
        void SaveLiterature(Literature literature, LiteratureEditViewModel literatureVM);
    }
}
