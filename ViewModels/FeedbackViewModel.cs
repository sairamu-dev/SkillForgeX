using System.ComponentModel.DataAnnotations;

namespace DevTaskFlow.ViewModels
{
    public class FeedbackViewModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "please enter the name")]
        public string Name { get; set; }
        [EmailAddress(ErrorMessage = " please enter valid email address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "please enter the message")]
        public string Message { get; set; }
    }
}
