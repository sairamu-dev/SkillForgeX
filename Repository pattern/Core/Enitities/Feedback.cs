using System.ComponentModel.DataAnnotations;

namespace SkillForgeX.Repository_pattern.Core.Enitities
{
    public class Feedback
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
    }
}
