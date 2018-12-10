using System.Collections.Generic;
using Portfolio_Strona.Models.ViewModels;

namespace Portfolio_Strona.Models
{
    public interface IProjectRepository
    {
        IEnumerable<Project> AllProjects { get; }
        Project GetOneProject(int projectID);

        void SaveProject(Project project, ProjectViewModel projectVM);

        Project DeleteProject(int projectID);
    }
}
