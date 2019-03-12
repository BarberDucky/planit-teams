using planit_data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planit_data.Repository
{
    public class UserRepository : GenericRepository<User>
    {
        public UserRepository(ApplicationContext context)
            : base(context)
        {

        }

        public User GetUserByUsername(string username)
        {
            List<User> users = Get(x => x.IdentificationUser.UserName == username)
                .ToList();

            if (users != null && users.Count > 0)
            {
                return users.First();
            }

            return null;
        }

        public bool Delete(string username)
        {
            User user = GetUserByUsername(username);

            if (user != null)
            {
                return (set.Remove(user) != null);
            }

            return false;
        }
    }
}
