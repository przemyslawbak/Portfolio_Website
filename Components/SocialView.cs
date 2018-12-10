using Microsoft.AspNetCore.Mvc;
using Portfolio_Strona.Models;
using System.Linq;

namespace Portfolio_Strona.Components
{
    public class SocialView : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        public SocialView (ApplicationDbContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var socialData = _context.ContactMe.FirstOrDefault();
            if (socialData == null)
            {
                //DO REPO
                socialData = new Contact
                {
                    PictureUrl = "#",
                    AboutMe1 = "empty",
                    AboutMe2 = "empty",
                    Phone = "empty",
                    Email = "#",
                    GitHub = "#",
                    LinkedIn = "#",
                    Facebook = "#"
                };
                _context.ContactMe.Add(socialData);
                _context.SaveChanges();
            }
            return View(socialData);
        }
    }
}
