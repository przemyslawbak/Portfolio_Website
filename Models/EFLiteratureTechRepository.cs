using System.Collections.Generic;
using System.Linq;

namespace Portfolio_Strona.Models
{
    public class EFLiteratureTechRepository : ILiteratureTechRepository
    {
        private ApplicationDbContext _context;
        public EFLiteratureTechRepository(ApplicationDbContext ctx)
        {
            _context = ctx;
        }
        public IEnumerable<LiteratureTechnology> LiteratureTechs => _context.LiteraturesTech.ToList();
        public LiteratureTechnology RemoveLiters(int literatureID)
        {
            foreach (var literatureTechs in _context.LiteraturesTech)
            {
                if (literatureTechs.LiteratureID == literatureID)
                {
                    _context.LiteraturesTech.Remove(literatureTechs);
                }
            }
            _context.SaveChanges();
            return null;
        }
        public LiteratureTechnology NewPair(LiteratureTechnology newPair)
        {
            _context.LiteraturesTech.Add(newPair);
            _context.SaveChanges();
            return null;
        }
    }
}
