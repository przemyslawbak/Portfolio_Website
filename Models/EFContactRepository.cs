using System.Collections.Generic;
using System.Linq;

namespace Portfolio_Strona.Models
{
    public class EFContactRepository : IContactRepository
    {
        private ApplicationDbContext _context;
        public EFContactRepository(ApplicationDbContext ctx)
        {
            _context = ctx;
        }
        public IEnumerable<Contact> Contacts => _context.ContactMe.ToList();
        public void SaveContact(Contact contact)
        {
            Contact myContact = new Contact();
            if (contact.ContactId == 0)
            {
                _context.ContactMe.Add(myContact);
            }
            else
            {
                myContact = _context.ContactMe.FirstOrDefault(a => a.ContactId == contact.ContactId);
            }
            myContact.AboutMe1 = contact.AboutMe1;
            myContact.AboutMe2 = contact.AboutMe2;
            myContact.Email = contact.Email;
            myContact.GitHub = contact.GitHub;
            myContact.LinkedIn = contact.LinkedIn;
            myContact.Phone = contact.Phone;
            myContact.PictureUrl = contact.PictureUrl;
            if (contact.Facebook == null)
            {
                myContact.Facebook = "#";
            }
            else
            {
                myContact.Facebook = contact.Facebook;
            }
            if (contact.GitHub == null)
            {
                myContact.GitHub = "#";
            }
            else
            {
                myContact.GitHub = contact.GitHub;
            }
            if (contact.LinkedIn == null)
            {
                myContact.LinkedIn = "#";
            }
            else
            {
                myContact.LinkedIn = contact.LinkedIn;
            }
            if (contact.PictureUrl == null || myContact.PictureUrl == "#")
            {
                myContact.PictureUrl = "#";
            }
            else
            {
                myContact.PictureUrl = contact.PictureUrl;
            }
            _context.SaveChanges();
        }
    }
}
