using System.Collections.Generic;

namespace Portfolio_Strona.Models
{
    public interface ILiteratureTechRepository
    {
        IEnumerable<LiteratureTechnology> LiteratureTechs { get; }
        LiteratureTechnology RemoveLiters(int literatureID);
        LiteratureTechnology NewPair(LiteratureTechnology newPair);
    }
}
