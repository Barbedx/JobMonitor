using Microsoft.AspNetCore.Identity;

namespace AspCoreAngular.Data
{
    public class ApplicationUser:IdentityUser
    {
        public int? FacebookId { get; set; }
        public int? GoogleID { get; set; }
    }
}
