using System.Collections.Generic;
using System.Linq;

namespace Portfolio_Strona.Models
{
    public class EFTechnologyRepository : ITechnologyRepository
    {
        private ApplicationDbContext _context;
        public EFTechnologyRepository(ApplicationDbContext ctx)
        {
            _context = ctx;
        }
        public IEnumerable<Technology> Technologies => _context.Technologies;
        public IEnumerable<Technology> TechnologiesByNames
        {
            get
            {
                var technologiesByName = from t in _context.Technologies
                                         orderby t.Name
                                         select t;
                return (technologiesByName);
            }
        }
        public void SaveTechnology(Technology technology)
        {
            if (technology.TechnologyID == 0)
            {
                _context.Technologies.Add(technology);
            }
            else
            {
                Technology dbEntry = _context.Technologies
                .FirstOrDefault(p => p.TechnologyID == technology.TechnologyID);
                if (dbEntry != null)
                {
                    dbEntry.Name = technology.Name;
                    if (technology.PictureLink == null)
                    {
                        dbEntry.PictureLink = "#";
                    }
                    else
                    {
                        dbEntry.PictureLink = technology.PictureLink;
                    }
                }
            }
            _context.SaveChanges();
        }
        public Technology DeleteTechnology(int technologyID)
        {
            Technology dbEntry = _context.Technologies
            .FirstOrDefault(p => p.TechnologyID == technologyID);
            if (dbEntry != null)
            {
                _context.Technologies.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
