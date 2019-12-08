using System.ComponentModel.DataAnnotations;

namespace Test.WebApi.ViewModel
{
    public class LoginViewModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}