using System.Collections.Generic;

namespace Portfolio_Strona.Models
{
    public interface ISurveyRepository
    {
        IEnumerable<Survey> Surveys { get; }
        void SaveSurvey(Survey survey);
    }
}
