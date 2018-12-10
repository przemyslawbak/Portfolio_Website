using System.Collections.Generic;

namespace Portfolio_Strona.Models
{
    public interface ITechnologyRepository
    {
        IEnumerable<Technology> Technologies { get; }
        void SaveTechnology(Technology technology);

        Technology DeleteTechnology(int technologyID);
        IEnumerable<Technology> TechnologiesByNames { get; }
    }
}
