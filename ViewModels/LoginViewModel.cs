using System.ComponentModel.DataAnnotations;

namespace DevTaskFlow.ViewModels
{
    public class LoginViewModel
    {
        [Required (ErrorMessage = "please enter valid username")]
        public string UserName {  get; set; }
        [Required(ErrorMessage = "please enter valid password")]
        public string Password { get; set; }
        public bool isRegisteredUser { get; set; }
    }
}
