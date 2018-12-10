using Microsoft.AspNetCore.Mvc;
using System;
using Portfolio_Strona.Models;
using Microsoft.AspNetCore.Authorization;

namespace Portfolio_Strona.Controllers
{
    public class SurveyController : Controller
    {
        private ISurveyRepository repositorySurv;
        public SurveyController(ISurveyRepository repoSurv)
        {
            repositorySurv = repoSurv;
        }
        [Authorize]
        public ActionResult Index() //Index
        {
            var surveyDb = repositorySurv.Surveys;
            return View(surveyDb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddComment(Survey survey)
        {
            if (survey == null)
            {
                return BadRequest();
            }
            survey.Time = DateTime.Now;
            repositorySurv.SaveSurvey(survey);

            return Ok();
        }
    }
}
