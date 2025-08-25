using Microsoft.AspNetCore.Mvc.Rendering;

namespace DevTaskFlow.Areas.Admin.ViewModels
{
    public class TaskViewModel
    {
        public int TaskID { get; set; }
        public int ProjectID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public int EstimatedDays { get; set; }
        public string RequiredSkills { get; set; }
        public string Status { get; set; }
        public int AssignedTo { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? CompletePercentage { get; set; }
        public DateTime? EndDate { get; set; }
    }

    public enum Projects
    {
        TeamForge = 1000,
        NextGen,
        WorkBridge,
        WorkNest,
        ProjectPulse
    }
}
