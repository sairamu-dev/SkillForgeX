using System.ComponentModel.DataAnnotations;

namespace DevTaskFlow.Repository_pattern.Core.Enitities
{
    public class KeywordWithSkills
    {
        [Key]
        public string keyword { get; set; }
        public string skills { get; set; }
    }
}
