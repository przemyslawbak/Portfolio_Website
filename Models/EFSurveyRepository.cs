using System.Collections.Generic;
using System.Linq;

namespace Portfolio_Strona.Models
{
    public class EFSurveyRepository : ISurveyRepository
    {
        private ApplicationDbContext _context;
        public EFSurveyRepository(ApplicationDbContext ctx)
        {
            _context = ctx;
        }
        public IEnumerable<Survey> Surveys => _context.Surveys;
        public void SaveSurvey(Survey survey)
        {
            Survey newSurvey = new Survey();
            if (survey.SurveyID == 0)
            {
                _context.Surveys.Add(newSurvey);
            }
            else
            {
                newSurvey = _context.Surveys.FirstOrDefault(a => a.SurveyID == survey.SurveyID);
            }
            newSurvey.Name = survey.Name;
            newSurvey.Change = survey.Change;
            newSurvey.Opinion = survey.Opinion;
            newSurvey.Time = survey.Time;
            _context.SaveChanges();
        }
    }
}
