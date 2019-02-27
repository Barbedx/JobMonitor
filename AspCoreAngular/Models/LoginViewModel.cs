using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
