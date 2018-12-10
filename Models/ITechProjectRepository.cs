using System.Collections.Generic;

namespace Portfolio_Strona.Models
{
    public interface ITechProjectRepository
    {
        IEnumerable<TechnologyProject> TechProjects { get; }
        TechnologyProject RemoveTechs(int projectID);
        TechnologyProject NewPair(TechnologyProject newPair);
    }
}
