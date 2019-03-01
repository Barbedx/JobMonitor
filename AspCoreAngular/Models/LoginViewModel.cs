using System.ComponentModel.DataAnnotations;

namespace AspCoreAngular.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username is requeired")]
        public string UserName { get; set; }

        [Required(ErrorMessage ="Password is requeired")]
        public string Password { get; set; }
    }
}
