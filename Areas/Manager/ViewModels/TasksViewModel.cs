using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DevTaskFlow.Areas.Manager.ViewModels
{
    public class TasksViewModel
    {
        public int ProjectID { get; set; }
        public string? ProjectName { get; set; }
        public int TaskID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public int EstimatedDays { get; set; }
        public int CreatedBy { get; set; }
        public int AssignedTo { get; set; }
        public string RequiredSkills { get; set; }
        public string Status { get; set; }
        public int? CompletePercentage { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

}
