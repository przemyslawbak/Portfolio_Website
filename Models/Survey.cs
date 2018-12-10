using System;

namespace Portfolio_Strona.Models
{
    public class Survey
    {
        public int SurveyID { get; set; }
        public string Name { get; set; }
        public string Opinion { get; set; }
        public string Change { get; set; }
        public DateTime Time { get; set; }
    }
}
