using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkillForgeX.Repository_pattern.Core.Enitities
{
    public class ErrorLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Error { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
