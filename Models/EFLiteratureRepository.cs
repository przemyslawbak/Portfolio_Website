using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Portfolio_Strona.Models.ViewModels;

namespace Portfolio_Strona.Models
{
    public class EFLiteratureRepository : ILiteratureRepository
    {
        private ApplicationDbContext _context;
        public EFLiteratureRepository(ApplicationDbContext ctx)
        {
            _context = ctx;
        }
        public IEnumerable<Literature> Literatures
        {
            get
            {
                var literatures = _context.Literatures.
                    Include(i => i.LiteraturesTechnologies).
                    ThenInclude(i => i.Technology).
                    AsEnumerable().
                    ToList();
                return (literatures);
            }
        }
        public Literature DeleteLiterature(int literatureID)
        {
            Literature literature = _context.Literatures
                .Include(i => i.LiteraturesTechnologies)
                .SingleOrDefault(i => i.LiteratureID == literatureID);
            if (literature != null)
            {
                _context.Literatures.Remove(literature);
                _context.SaveChanges();
            }
            return null;
        }
        public void SaveLiterature(Literature literature, LiteratureEditViewModel literatureVM)
        {
            {
                //repo save
                literature.Title = literatureVM.Title;
                literature.Authors = literatureVM.Authors;
                if (literatureVM.Url == null)
                {
                    literature.Url = "#";
                }
                else
                {
                    literature.Url = literatureVM.Url;
                }
            }
            if (literatureVM.LiteratureID == 0)
            {
                _context.Literatures.Add(literature);
            }
            _context.SaveChanges();
        }    }
}
