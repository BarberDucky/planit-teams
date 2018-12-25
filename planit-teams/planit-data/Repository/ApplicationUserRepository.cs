using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planit_data.Repository
{
    public class ApplicationUserRepository
    {
        private ApplicationContext context;

        public ApplicationUserRepository(ApplicationContext context)
        {
            this.context = context;
        }
        public ApplicationUser AddApplicationUser(String username, String email, String password)
        {
            var store = new UserStore<ApplicationUser>(context);
            var manager = new UserManager<ApplicationUser>(store);

            if (username == null || email == null || password == null)
                return null;

            var user = new ApplicationUser()
            {
                Email = email,
                UserName = username
            };
            var result = manager.Create(user, password);
            if(result.Succeeded)
            {
                return user;
            }
            else
            {
                return null;
            }
        }
    }
}
