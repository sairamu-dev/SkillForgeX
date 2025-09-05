namespace SkillForgeX.Repository_pattern.Core.Enitities
{
    public class Api
    {
        public int Id { get; set; }
        public string ApiName { get; set; }
        public string ApiKey { get; set; }
        public string Month {  get; set; }
        public int UsageCount { get; set; }
        public bool IsActive { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
