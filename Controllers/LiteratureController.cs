using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Portfolio_Strona.Models;
using Portfolio_Strona.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Portfolio_Strona.Controllers
{
    public class LiteratureController : Controller
    {
        //index page size
        public int PageSize = 8;
        //literature repo
        private ILiteratureRepository repositoryLit;
        private ITechnologyRepository repositoryTech;
        private ILiteratureTechRepository repositoryLitTech;
        //viewmodel instance
        LiteratureEditViewModel literatureVM = new LiteratureEditViewModel();
        //constructor and instances for repo
        public LiteratureController(ILiteratureRepository repoLit, ITechnologyRepository repoTech, ILiteratureTechRepository repoLitTech)
        {
            repositoryLit = repoLit; //repository
            repositoryTech = repoTech;
            repositoryLitTech = repoLitTech;
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
            //selecting item
            if (techQuery != null) //if db repo var is not null...
            {
                return techQuery.Select(n => new SelectListItem { Text = n.Name, Value = n.TechnologyID.ToString() }).ToList();
                //...returning populated dropdown list to the view
            }
            return null;
        }
        //GET Index
        public ActionResult Index(int thePage = 1) //Index
        {
            //VM instance
            LiteratureIndexViewModel literatureVM = new LiteratureIndexViewModel
            {
                LiteratureList = repositoryLit.Literatures.AsQueryable()
                  //with projects relations
                  .Include(i => i.LiteraturesTechnologies)
                    .ThenInclude(i => i.Technology)
                  .OrderBy(i => i.Title)
                  .Skip((thePage - 1) * PageSize)
                  .Take(PageSize),
                PagingInfo = new PagingInfoViewModel
                {
                    CurrentPage = thePage,
                    ItemsPerPage = PageSize,
                    TotalItems = repositoryLit.Literatures.Count()
                }
            };

            return View(literatureVM);
        }
        //GET Edit
        [Authorize]
        public ActionResult Edit(int? literatureId)
        {
            if (literatureId == null)
            {
                return NotFound();
            }
            var literatureDb = repositoryLit.Literatures.AsQueryable()
                .Include(i => i.LiteraturesTechnologies)
                .ThenInclude(i => i.Technology)
                .AsNoTracking()
                .SingleOrDefault(m => m.LiteratureID == literatureId);
            if (literatureDb == null)
            {
                return NotFound();
            }
            var literatureTechnologies = literatureDb.LiteraturesTechnologies.Select(c => c.TechnologyID);
            //repo load
            literatureVM.LiteratureID = literatureDb.LiteratureID;
            literatureVM.Title = literatureDb.Title;
            literatureVM.Authors = literatureDb.Authors;
            literatureVM.Url = literatureDb.Url;
            literatureVM.TechnologyIds = literatureTechnologies.ToArray(); //array Clinets z CPs
            literatureVM.Technologies = PopulateTechnologiesDropdown(); //populate
            return View(literatureVM); //zwróć VM
        }
        //POST Edit
        [Authorize]
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LiteratureEditViewModel literatureVM)
        {
            if(ModelState.IsValid)
            {
                if (literatureVM == null)
                {
                    return RedirectToAction(nameof(Index));
                }

                literatureVM.Technologies = PopulateTechnologiesDropdown();//productVM.Products przybiera to co zwraca dropdown
                int? literatureID = literatureVM.LiteratureID;
                //passing dropdown selected items to the collection
                List<SelectListItem> selectedItems = literatureVM.Technologies.Where(p => literatureVM.TechnologyIds.Contains(int.Parse(p.Value))).ToList();
                Literature literature;
                //new literature
                if (literatureID == 0)
                {
                    literature = new Literature();
                }
                //edit literature
                else
                {
                    literature = repositoryLit.Literatures.FirstOrDefault(o => o.LiteratureID == literatureID);
                }
                repositoryLit.SaveLiterature(literature, literatureVM);

                //remove all technologies for this literature to avoid duplicated keys
                repositoryLitTech.RemoveLiters(literatureVM.LiteratureID);
                //new tech list for literature
                if (selectedItems != null)
                {
                    //make new technology list for this literature
                    literature.LiteraturesTechnologies = new List<LiteratureTechnology>();
                    foreach (var item in selectedItems)
                    {
                        var literatureAdd = new LiteratureTechnology { LiteratureID = literature.LiteratureID, TechnologyID = int.Parse(item.Value) };
                        repositoryLitTech.NewPair(literatureAdd);
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                literatureVM.Technologies = PopulateTechnologiesDropdown();
                return View(literatureVM);
            }
        }
        [Authorize]
        public ViewResult Create()
        {
            var newLiterature = new LiteratureEditViewModel
            {
                TechnologyIds = new int[] { },
                Title = "Title of the book",
                Url = "#",
                Authors = "Authors of the book",
                Technologies = PopulateTechnologiesDropdown()
            };
            return View("Edit", newLiterature);
        }
        [Authorize]
        [HttpPost]
        public IActionResult Delete(int literatureID)
        {
            //repo delete
            repositoryLit.DeleteLiterature(literatureID);
            return RedirectToAction(nameof(Index));
        }
    }
}
