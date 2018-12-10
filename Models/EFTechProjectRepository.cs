using System.Collections.Generic;
using System.Linq;

namespace Portfolio_Strona.Models
{
    public class EFTechProjectRepository : ITechProjectRepository
    {
        private ApplicationDbContext _context;
        public EFTechProjectRepository(ApplicationDbContext ctx)
        {
            _context = ctx;
        }
        public IEnumerable<TechnologyProject> TechProjects => _context.TechProjects.ToList();

        public TechnologyProject RemoveTechs (int projectID)
        {
            foreach (var techproject in _context.TechProjects)
            {
                if (techproject.ProjectID == projectID)
                {
                    _context.TechProjects.Remove(techproject);
                }
            }
            _context.SaveChanges();
            return null;
        }
        public TechnologyProject NewPair (TechnologyProject newPair)
        {
            _context.TechProjects.Add(newPair);
            _context.SaveChanges();
            return null;
        }
    }
}
