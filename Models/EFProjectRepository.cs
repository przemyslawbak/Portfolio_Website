using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Portfolio_Strona.Models.ViewModels;

namespace Portfolio_Strona.Models
{
    public class EFProjectRepository : IProjectRepository
    {
        private ApplicationDbContext _context;
        public EFProjectRepository(ApplicationDbContext ctx)
        {
            _context = ctx;
        }
        public IEnumerable<Project> AllProjects
        {
            get
            {
                var projects = _context.Projects.Include(i => i.TechnologiesProjects).AsEnumerable().ToList();
                return (projects);
            }
        }
        public void SaveProject(Project project, ProjectViewModel projectVM)
        {
            {
                project.Name = projectVM.Name;
                if (projectVM.PictureUrl == null)
                {
                    project.PictureUrl = "#";
                }
                else
                {
                    project.PictureUrl = projectVM.PictureUrl;
                }
                project.BackColor = projectVM.BackColor;
                project.Comments = projectVM.Comments;
                if (projectVM.WebLink == null)
                {
                    project.WebUrl = "#";
                }
                else
                {
                    project.WebUrl = projectVM.WebLink;
                }
                if (projectVM.GitHubLink == null)
                {
                    project.GitHubUrl = "#";
                }
                else
                {
                    project.GitHubUrl = projectVM.GitHubLink;
                }
                if (projectVM.WorkLogUrl == null)
                {
                    project.WorkLogUrl = "#";
                }
                else
                {
                    project.WorkLogUrl = projectVM.WorkLogUrl;
                }
                if (projectVM.YouTubeUrl == null)
                {
                    project.YouTubeUrl = "#";
                }
                else
                {
                    project.YouTubeUrl = projectVM.YouTubeUrl;
                }
                project.CompletionDate = projectVM.CompletionDate;
            }
            if (projectVM.ProjectID == 0)
            {
                _context.Projects.Add(project);
            }
            _context.SaveChanges();
        }
        public Project DeleteProject(int projectID)
        {
            Project project = _context.Projects
                .Include(i => i.TechnologiesProjects)
                .SingleOrDefault(i => i.ProjectID == projectID);
            if (project != null)
            {
                _context.Projects.Remove(project);
                _context.SaveChanges();
            }
            return null;
        }
        public Project GetOneProject(int projectID)
        {
            Project project = _context.Projects
                .Include(i => i.TechnologiesProjects)
                .ThenInclude(i => i.Technology)
                .SingleOrDefault(i => i.ProjectID == projectID);
            return project;
        }
    }
}
