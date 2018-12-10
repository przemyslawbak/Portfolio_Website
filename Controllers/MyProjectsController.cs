using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Portfolio_Strona.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Portfolio_Strona.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Portfolio_Strona.Controllers
{
    public class MyProjectsController : Controller
    {
        //project repo
        private IProjectRepository repositoryProj;
        private ITechnologyRepository repositoryTech;
        private ITechProjectRepository repositoryTechProj;
        //viewmodel instance
        ProjectViewModel projectVM = new ProjectViewModel();
        //constructor and instances for repo
        public MyProjectsController(IProjectRepository repoProj, ITechnologyRepository repoTech, ITechProjectRepository repoTechProj)
        {
            repositoryProj = repoProj; //repository project
            repositoryTech = repoTech;
            repositoryTechProj = repoTechProj;
        }
        //populating dropdown list
        private List<SelectListItem> PopulateTechnologiesDropdown() //method populating dropdown list
        {
            var techQuery = repositoryTech.TechnologiesByNames;
            if (techQuery == null)
            {
                Technology newTech = new Technology
                {
                    Name = "change my name",
                    PictureLink = "#"
                };
                repositoryTech.SaveTechnology(newTech);
            }
            if (techQuery != null) //if db repo var is not null...
            {
                return techQuery.Select(n => new SelectListItem { Text = n.Name, Value = n.TechnologyID.ToString() }).ToList();
                //...returning populated dropdown list to the view
            }
            return null;
        }
        //GET Index
        public ViewResult Index()
        {
            var projects = repositoryProj.AllProjects.OrderByDescending(i => i.CompletionDate);
            //returning db repo for projects
            return View(projects);
        }
        //GET Edit
        [Authorize]
        public ActionResult Edit(int? projectId)
        {
            if (projectId == null)
            {
                return NotFound();
            }
            var projectDb = repositoryProj.AllProjects.AsQueryable()
                .Include(i => i.TechnologiesProjects)
                .ThenInclude(i => i.Technology)
                .AsNoTracking()
                .SingleOrDefault(m => m.ProjectID == projectId);
            if (projectDb == null)
            {
                return NotFound();
            }
            var projectsTechnologies = projectDb.TechnologiesProjects.Select(c => c.TechnologyID);
            //repo load
            projectVM.ProjectID = projectDb.ProjectID;
            projectVM.Name = projectDb.Name;
            projectVM.PictureUrl = projectDb.PictureUrl;
            projectVM.BackColor = projectDb.BackColor;
            projectVM.Comments = projectDb.Comments;
            projectVM.WebLink = projectDb.WebUrl;
            projectVM.GitHubLink = projectDb.GitHubUrl;
            projectVM.YouTubeUrl = projectDb.YouTubeUrl;
            projectVM.WorkLogUrl = projectDb.WorkLogUrl;
            projectVM.CompletionDate = projectDb.CompletionDate;
            projectVM.TechnologyIds = projectsTechnologies.ToArray(); //array Clinets z CPs
            projectVM.Technologies = PopulateTechnologiesDropdown(); //populate
            return View(projectVM); //zwróć VM
        }
        //POST Edit
        [Authorize]
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProjectViewModel projectVM)
        {
            if (ModelState.IsValid)
            {
                if (projectVM == null)
                {
                    return RedirectToAction(nameof(Index));
                }

                projectVM.Technologies = PopulateTechnologiesDropdown();//productVM.Products przybiera to co zwraca dropdown
                int? projectID = projectVM.ProjectID;
                //passing dropdown selected items to the collection
                List<SelectListItem> selectedItems = projectVM.Technologies.Where(p => projectVM.TechnologyIds.Contains(int.Parse(p.Value))).ToList();
                Project project;
                //for new project
                if (projectID == 0)
                {
                    project = new Project();
                }
                //for edit project
                else
                {
                    //repo get project
                    project = repositoryProj.AllProjects.FirstOrDefault(o => o.ProjectID == projectID);
                }
                //repo save project
                repositoryProj.SaveProject(project, projectVM);
                //remove all technologies for this project to avoid duplicated keys
                repositoryTechProj.RemoveTechs(projectVM.ProjectID);
                //new tech list for project
                if (selectedItems != null)
                {
                    //make new technology list for this project
                    project.TechnologiesProjects = new List<TechnologyProject>();
                    foreach (var item in selectedItems)
                    {
                        var technologyAdd = new TechnologyProject { ProjectID = project.ProjectID, TechnologyID = int.Parse(item.Value) };
                        //repo new pair project - technology
                        repositoryTechProj.NewPair(technologyAdd);
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                projectVM.Technologies = PopulateTechnologiesDropdown();
                return View(projectVM);
            }
        }
        [Authorize]
        public ViewResult Create()
        {
            var newProject = new ProjectViewModel
            {
                TechnologyIds = new int[] { },
                GitHubLink = "#",
                PictureUrl = "/src/img/projects/default.png",
                BackColor = "#fafafa",
                Comments = "Few words about the project",
                Name = "Name of the project",
                WebLink = "#",
                WorkLogUrl = "#",
                YouTubeUrl = "#",
                CompletionDate = DateTime.Now,
                Technologies = PopulateTechnologiesDropdown()
            };
            return View("Edit", newProject);
        }
        [HttpPost]
        [Authorize]
        public IActionResult Delete(int projectID)
        {
            //repo delete
            repositoryProj.DeleteProject(projectID);
            return RedirectToAction(nameof(Index));
        }
        public ActionResult Details(int projectID)
        {
            Project project = repositoryProj.GetOneProject(projectID);
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }
        public ViewResult Error()
        {
            return View();
        }
    }
}
