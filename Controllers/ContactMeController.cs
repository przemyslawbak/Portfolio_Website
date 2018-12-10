using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Portfolio_Strona.Models;
using Microsoft.AspNetCore.Authorization;

namespace Portfolio_Strona.Controllers
{
    //no repo used
    public class ContactMeController : Controller
    {
        private IContactRepository repository;
        public ContactMeController(IContactRepository repo)
        {
            repository = repo; //repository
        }
        public ActionResult Index()
        {
            Contact aboutMeDb = repository.Contacts.FirstOrDefault();
            if (aboutMeDb == null)
            {
                aboutMeDb = new Contact
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
                repository.SaveContact(aboutMeDb);
            }
            return View(aboutMeDb);
        }
        [Authorize]
        public ActionResult Edit()
        {
            Contact aboutMeDb = repository.Contacts.FirstOrDefault();
            if (aboutMeDb == null)
            {
                aboutMeDb = new Contact
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
                repository.SaveContact(aboutMeDb);
            }
            return View(aboutMeDb);
        }
        [Authorize]
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Contact modelReturned)
        {
            Contact aboutMeDb = repository.Contacts.FirstOrDefault();
            aboutMeDb = modelReturned;
            repository.SaveContact(aboutMeDb);
            return RedirectToAction(nameof(Index));
        }

    }
}
