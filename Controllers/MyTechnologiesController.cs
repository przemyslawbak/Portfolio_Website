using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Portfolio_Strona.Models;
using Portfolio_Strona.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Portfolio_Strona.Controllers
{
    public class MyTechnologiesController : Controller
    {
        //index page size
        public int PageSize = 8;
        //repository
        private ITechnologyRepository repositoryTech;
        public MyTechnologiesController (ITechnologyRepository repoTech)
        {
            repositoryTech = repoTech;
        }
        public async Task<IActionResult> Index(int thePage = 1) //Index
        {

            //VM instance
            TechnologyViewModel technologyVM = new TechnologyViewModel
            {
                Technologies = await repositoryTech.Technologies.AsQueryable()
                  //with projects relations
                  .Include(i => i.TechnologiesProjects)
                    .ThenInclude(i => i.Project)
                  //with literature relations
                  .Include(i => i.LiteraturesTechnologies)
                    .ThenInclude(i => i.Literature)
                  .OrderBy(i => i.Name)
                  .Skip((thePage - 1) * PageSize)
                  .Take(PageSize)
                  .ToListAsync(),
                PagingInfo = new PagingInfoViewModel
                {
                    CurrentPage = thePage,
                    ItemsPerPage = PageSize,
                    TotalItems = repositoryTech.Technologies.Count()
                }
            };

            return View(technologyVM);
        }
        public IActionResult Edit(int technologyId)
        {
            var technology = repositoryTech.Technologies
                .SingleOrDefault(t => t.TechnologyID == technologyId);
            return View(technology);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Technology technology)
        {
            if (ModelState.IsValid)
            {
                repositoryTech.SaveTechnology(technology);
                return RedirectToAction("Index");
            }
            else
            {
                return View(technology);
            }
        }
        [Authorize]
        [HttpPost]
        public IActionResult Delete(int technologyID)
        {
            Technology deleteTechnology = repositoryTech.DeleteTechnology(technologyID);
            return RedirectToAction("Index");
        }
        [Authorize]
        public ViewResult Create()
        {
            var newTech = new Technology
            {
                Name = "Name of technology",
                PictureLink = "#"
            };
            return View("Edit", newTech);
        }
    }
}
