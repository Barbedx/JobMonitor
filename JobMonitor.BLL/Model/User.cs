using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobMonitor.BLL.Model
{
    public class User :IdentityUser
    {
        public long? FacebookId { get; set; }
        
    }
}
