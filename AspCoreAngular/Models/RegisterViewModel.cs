using System.ComponentModel.DataAnnotations;

namespace AspCoreAngular.Models
{
    public class RegisterViewModel : LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get;   set; }
    }
}
