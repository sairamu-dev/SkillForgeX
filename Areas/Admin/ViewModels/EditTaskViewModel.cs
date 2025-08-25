using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DevTaskFlow.Areas.Admin.ViewModels
{
    public class EditTaskViewModel
    {
        public int TaskID { get; set; }
        [Display(Name = "Project")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid project.")]
        public int ProjectID { get; set; }
        [Required(ErrorMessage = "please provide a title")]
        public string Title { get; set; }
        [Required(ErrorMessage = "please provide a Description about the task")]
        public string Description { get; set; }
        [Required(ErrorMessage = "please select a priority level")]
        public string Priority { get; set; }
        public int EstimatedDays { get; set; }
        public string? RequiredSkills { get; set; }
        public string Status { get; set; }
        [Display(Name = "AssignedTo")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a developer to assign a task.")]
        public int AssignedTo { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? CompletePercentage { get; set; }
        [Required(ErrorMessage = "Please select an end date")]
        public DateTime? EndDate { get; set; }
        [Required(ErrorMessage = "please select required skills to complete the task")]
        public List<string> RequiredSkillsList { get; set; }
        public List<SelectListItem> Projects { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Skills { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> PriorityList { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> StatusList { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> availableDevUsers { get; set; } = new List<SelectListItem>();
    }
}
