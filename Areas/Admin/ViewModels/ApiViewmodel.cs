using System.ComponentModel.DataAnnotations;

namespace DevTaskFlow.Areas.Admin.ViewModels
{
    public class ApiViewmodel
    {
        public int Id { get; set; }
        public string ApiName { get; set; }
        [Required(ErrorMessage ="Please provide a month")]
        public string Month { get; set; }
        public int UsageCount { get; set; }
        public bool IsActive { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
