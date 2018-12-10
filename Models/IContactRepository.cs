using System.Collections.Generic;

namespace Portfolio_Strona.Models
{
    public interface IContactRepository
    {
        IEnumerable<Contact> Contacts { get; }
        void SaveContact(Contact contact);
    }
}
