using Microsoft.EntityFrameworkCore;

namespace Portfolio_Strona.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>
            options) : base(options) { }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Technology> Technologies { get; set; }
        public DbSet<Literature> Literatures { get; set; }
        public DbSet<LiteratureTechnology> LiteraturesTech { get; set; }
        public DbSet<TechnologyProject> TechProjects { get; set; }
        public DbSet<Contact> ContactMe { get; set; }
        public DbSet<Survey> Surveys { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LiteratureTechnology>()
                .HasKey(c => new { c.LiteratureID, c.TechnologyID });

            modelBuilder.Entity<TechnologyProject>()
                .HasKey(c => new { c.TechnologyID, c.ProjectID });
        }
    }
}
