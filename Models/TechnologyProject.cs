namespace Portfolio_Strona.Models
{
    //join model many-to-many
    public class TechnologyProject
    {
        public int ProjectID { get; set; }
        public int TechnologyID { get; set; }
        public Project Project { get; set; }
        public Technology Technology { get; set; }
    }
}
