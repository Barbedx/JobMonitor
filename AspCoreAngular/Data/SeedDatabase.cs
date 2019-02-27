using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspCoreAngular.Data
{
    public class SeedDatabase 
    {

        public static void Initialize(IServiceProvider serviceProvider)
        {
            //var context = serviceProvider.GetRequiredService<SqlJobMonitorContext>();
            //var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            //context.Database.EnsureCreated();
            //if (!context.Users.Any())
            //{
            //    var user = new ApplicationUser()
            //    {
            //        Email = "my@email.com",
            //        SecurityStamp = Guid.NewGuid().ToString(),
            //        UserName = "myname"
            //    };
            //    userManager.CreateAsync(user, "Password@12");
            //}

        }
    }
}
