using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspCoreAngular.Data
{
    public class ApplicationUser:IdentityUser
    {
        public int? FacebookId { get; set; }
        public int? GoogleID { get; set; }
    }
}
