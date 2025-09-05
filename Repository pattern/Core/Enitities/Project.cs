namespace SkillForgeX.Repository_pattern.Core.Enitities
{
    public class Project
    {
        public int ID { get; set; }
        public string ProjectName { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
