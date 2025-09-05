using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace SkillForgeX.Areas.Manager.ViewModels
{
    public class CreateTaskViewmodel
    {
        [Display(Name = "Project")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid project.")]
        public int ProjectID { get; set; } = 0;

        [Required(ErrorMessage = "please provide a title")]
        public string Title { get; set; }
        [Required(ErrorMessage = "please provide a Description about the task")]
        public string Description { get; set; }

        [Required(ErrorMessage = "please select a priority level")]
        public string Priority { get; set; }

        public int EstimatedDays { get; set; }

        //[Required(ErrorMessage = "please select required skills to complete the task")]
        public List<string>? RequiredSkillsList { get; set; }
        public string? RequiredSkills { get; set; }
        public int? CreatedBy { get; set; }
        public int? AssignedTo { get; set; }
        public int? TaskID { get; set; }
        public string Status { get; set; } = "Pending";
        [Required(ErrorMessage = "Please select an end date")]
        public DateTime EndDate { get; set; }
        public List<SelectListItem> Projects { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Skills { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> PriorityList { get; set; } = new List<SelectListItem>();
    }

    //public class keywordWithSkills
    //{
    //    public string keyword { get; set; }
    //    public string Skills { get; set; }
    //}
}
